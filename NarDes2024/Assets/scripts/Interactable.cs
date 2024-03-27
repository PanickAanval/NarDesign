using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public Sprite interactIcon;
    public int ID;
   
    void Start()
    {
        ID = Random.Range(0, 999999);
    }

    public void TaskDone()
    {
        Debug.Log("TaskIsDone");
    }


}
