using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public TaskList tasks;
    //public TaskKeeper keeper;

    public UnityEvent OnInteract;
    public Sprite interactIcon;
    public int ID;

    public GameObject This;

    public GameObject Pan;
    public GameObject PanNew;
    public GameObject PanBox;

    public GameObject CallComplete;
    public GameObject CatComplete;
    public GameObject OnStoveComplete;
    public GameObject OffStoveComplete;
    public GameObject DishesComplete;
    public GameObject BedCompelte;
    public GameObject ComComplete;
    public GameObject FireCompelte;
    public GameObject DoorComplete;

    float timer = 30;

    void Start()
    {
        ID = Random.Range(0, 999999);

        GameObject.Find("keeper");
        TaskKeeper.keeper.GetComponent<TaskKeeper>();
    }

    private void Update()
    {
        if(TaskKeeper.keeper.DidPan1 == 1 && TaskKeeper.keeper.DidPan2 < 1)
        {
         // Debug.Log(timer);
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimer(timer);
            }
    else
            {
                Debug.Log("Pan Is Ready");
                PanBox.SetActive(true);
            }
        }
    }

    public void TaskDone()
    {
        //put here what should happen when the task is done

        This.SetActive(false);

        Debug.Log("TaskIsDone");

        if (name == "PhoneSource")
        {
            TaskKeeper.keeper.DidPhone = 1;
            TaskKeeper.keeper.PlayerCalledMom = TaskKeeper.keeper.PlayerCalledMom + 1;
            tasks.Tasks.Remove("CallMom");
            tasks.callMom.SetActive(true);
            CallComplete.SetActive(true);
        }
        if (name == "CatSource")
        {
            TaskKeeper.keeper.DidCat = 1;

            tasks.Tasks.Remove("FeedCat");
            tasks.feedCat.SetActive(true);
            CatComplete.SetActive(true);
        }
        if (name == "PanSource")
        {



            TaskKeeper.keeper.DidPan1 = 1;

            tasks.Tasks.Remove("OnStove");
            tasks.onStove.SetActive(true);
            OnStoveComplete.SetActive(true);

            Pan.SetActive(false);
            This.SetActive(false);

            PanNew.SetActive(true);
            PanBox.SetActive(false);

        }

            if (name == "PanSource2")
            {
                Debug.Log("Hello");
                TaskKeeper.keeper.DidPan2 = 1;

                PanNew.SetActive(false);
                PanBox.SetActive(false);
                tasks.Tasks.Remove("OffStove");
                tasks.offStove.SetActive(true);
                OffStoveComplete.SetActive(true);
            }
        
        if (name == "DishseSource") 
        {
            TaskKeeper.keeper.DidDishes = 1;

            tasks.Tasks.Remove("DoDishes");
            tasks.doDishes.SetActive(true);
            DishesComplete.SetActive(true);
        }
        if (name == "BedSource")
        {
            TaskKeeper.keeper.DidBed = 1;

            tasks.Tasks.Remove("MakeBed");
            tasks.makeBed.SetActive(true);
            BedCompelte.SetActive(true);
        }
        if (name == "ComSource")
        {
           TaskKeeper.keeper.DidCom = 1;

            tasks.Tasks.Remove("PayBills");
            tasks.payBills.SetActive(true);
            ComComplete.SetActive(true);
        }
        if (name == "FireSource")
        {
            TaskKeeper.keeper.DidFire = 1;

            tasks.Tasks.Remove("FireAlarm");
            tasks.fireAlarm.SetActive(true);
            FireCompelte.SetActive(true);
        }
        if (name == "DoorSource")
        {
            TaskKeeper.keeper.DidDoor = 1;

            tasks.Tasks.Remove("BuyPresent");
            tasks.buyPresent.SetActive(true);
            DoorComplete.SetActive(true);
        }
    }

    void UpdateTimer(float CurrentTime)
    {
        CurrentTime += 1;

        float minutes = Mathf.FloorToInt(CurrentTime / 60);
        float seconds = Mathf.FloorToInt(CurrentTime % 60);
    }
}
