using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    public int digit;

    private void OnTriggerExit(Collider other)
    {
        digit = Random.Range(0, 101);
        if(digit <= 35)
        {
            //play dialogue of quinn talking to themselves
        }
    }
}
