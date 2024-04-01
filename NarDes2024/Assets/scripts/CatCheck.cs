using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCheck : MonoBehaviour
{
    //public TaskKeeper keeper;

    public GameObject cat;

    void Start()
    {
        GameObject.Find("keeper");
        TaskKeeper.keeper.GetComponent<TaskKeeper>();

        if (TaskKeeper.keeper.CatRanAway == 1)
        {
            cat.SetActive(false);
        } 
    }
}
