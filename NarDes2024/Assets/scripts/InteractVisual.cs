using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractVisual : MonoBehaviour
{
    public Interactor interactor;

    public float holdDuration;
    public Image fillCircle;

    private float holdTimer = 0;
    private bool isHolding = false;

    public GameObject circle;

    private void Start()
    {
        holdDuration = interactor.timeBetweenTimers;
    }

    private void Update()
    {
            if (isHolding)
            {
                holdTimer += Time.deltaTime;
                fillCircle.fillAmount = holdTimer / holdDuration;
                if (holdTimer >= holdDuration)
                {
                    //normally put something here to do an action but in this case that is not neccesary since its purely a visual indicator
                }
            }
        
    }

    public void onHold(InputAction.CallbackContext context)
    {

        if(context.started)
        {
            isHolding = true;
        } else if(context.canceled)
        {
            ResetHold();
        }
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
        fillCircle.fillAmount = 0f;
    }
}
