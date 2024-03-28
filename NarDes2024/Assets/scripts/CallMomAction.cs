using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMomAction : MonoBehaviour
{
    public TaskList taskList;
    public GameObject OffStove;

    void ConvoMomEnds()
    {
        if( taskList.Tasks.Contains("OffStove"))
            {
            Debug.Log("OffStove is still enabled");
        } else
        {
            taskList.Tasks.Add("OffStove");
            OffStove.SetActive(true);
        }
    }
}
