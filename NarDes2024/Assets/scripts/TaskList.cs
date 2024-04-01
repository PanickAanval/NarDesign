using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    //public TaskKeeper keeper;

    public List<string> Tasks = new List<string>();

    public float TimeLeft = 40;
    float SpecialTimer = 20;

    public GameObject onStove;
    public GameObject offStove;
    public GameObject feedCat;
    public GameObject doDishes;
    public GameObject makeBed;
    public GameObject callMom;
    public GameObject payBills;
    public GameObject fireAlarm;
    public GameObject buyPresent;

    void Start()
    {
        GameObject.Find("keeper");
        TaskKeeper.keeper.GetComponent<TaskKeeper>();

        Tasks.Add("OnStove");
        Tasks.Add("OffStove");
        Tasks.Add("FeedCat");
        Tasks.Add("DoDishes");
        Tasks.Add("MakeBed");
        Tasks.Add("CallMom");
        Tasks.Add("PayBills");
        Tasks.Add("FireAlarm");
        Tasks.Add("BuyPresent");

        if(TaskKeeper.keeper.OffStoveGetsRemovedFaster == 1)
        {
            RemoveOffStove();
        }

        Tasks.Add("InCaseOfAllTasksGone");
    }

    void PickRandomRemove(List<string> listToRandomize)
    {
        GetRandomItem(Tasks);

        string GetRandomItem(List<string> listToRandomize)
        {
            int randomNum = Random.Range(0, listToRandomize.Count);
            string printRandom = listToRandomize[randomNum];
            print(printRandom);

            if(printRandom == "InCaseOfAllTasksGone")
            {
                Tasks.Add("InCaseOfAllTasksGone");
            }

            Tasks.Remove(printRandom);

            if(printRandom == "OnStove")
            {
                onStove.SetActive(false);
            }
            if (printRandom == "OffStove")
            {
                offStove.SetActive(false);
            }
            if (printRandom == "FeedCat")
            {
                feedCat.SetActive(false);
            }
            if (printRandom == "DoDishes")
            {
                doDishes.SetActive(false);
            }
            if (printRandom == "MakeBed")
            {
                makeBed.SetActive(false);
            }
            if (printRandom == "CallMom")
            {
                callMom.SetActive(false);
            }
            if (printRandom == "PayBills")
            {
                payBills.SetActive(false);
            }
            if (printRandom == "FireAlarm")
            {
                fireAlarm.SetActive(false);
            }
            if (printRandom == "BuyPresent")
            {
                buyPresent.SetActive(false);
            }
            

            return printRandom;
        }
    }

    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            UpdateTimer(TimeLeft);
        }
        else
        {
            TimeLeft = 20;
            PickRandomRemove(Tasks);
        }
    }

    void UpdateTimer(float CurrentTime)
    {
        CurrentTime += 1;

        float minutes = Mathf.FloorToInt(CurrentTime / 60);
        float seconds = Mathf.FloorToInt(CurrentTime % 60);
    }

    public void RemoveOffStove()
    {
        if (SpecialTimer > 0)
        {
            SpecialTimer -= Time.deltaTime;
            UpdateTimer(SpecialTimer);
        }
        else
        {
            Tasks.Remove("OffStove");
            offStove.SetActive(false);
        }
    }
}
