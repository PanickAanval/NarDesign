using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
   // public TaskKeeper keeper;

    public LayerMask interactableLayerMask = 8;

    public Interactable interactable;
    public Image interactimage;
    public Sprite defaultIcon;
    public Sprite defaultInteractIcon;

    float timer1 = 0f;
    float timer2 = 0f;
    public float timeBetweenTimers = 15f;

    public GameObject circle;

    private void Start()
    {
        circle.SetActive(false);

        if (timeBetweenTimers != 7f)
        {
            timeBetweenTimers = 7f;
        }

        GameObject.Find("keeper");
        TaskKeeper.keeper.GetComponent<TaskKeeper>();
    }

    private void Update()
    {

        if(TaskKeeper.keeper.InteractTimerIncreases == 1)
        {
            if (timeBetweenTimers != 10f)
            {
                timeBetweenTimers = 10f;
            }
        }
        if(TaskKeeper.keeper.InteractTimerIncreases == 2)
        {
            if (timeBetweenTimers != 15f)
            {
                timeBetweenTimers = 15f;
            }
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactableLayerMask))
        {
            
            circle.SetActive(true);
            if (hit.collider.GetComponent<Interactable>() != false)
            {
                if (interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                }
                if (interactable.interactIcon != null)
                {
                    interactimage.sprite = interactable.interactIcon;
                }
                else
                {
                    interactimage.sprite = defaultInteractIcon;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    timer1 = Time.time;

                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    timer2 = Time.time;
                    if (timer2 - timer1 >= timeBetweenTimers)
                    {
                        timer1 = 0f;
                        timer2 = 0f;

                        interactable.OnInteract.Invoke();
                    }
                    else
                    {
                        timer1 = 0f;
                        timer2 = 0f;
                    }
                }
            }
        }
        else
        {
            if (interactimage.sprite != defaultIcon)
            {
                interactimage.sprite = defaultIcon;
                circle.SetActive(false);
                Debug.Log("Default");
            }
        }
    }
}
