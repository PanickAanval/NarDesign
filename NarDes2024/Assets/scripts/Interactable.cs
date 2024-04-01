using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public TaskList tasks;
    public TaskKeeper keeper;

    public UnityEvent OnInteract;
    public Sprite interactIcon;
    public int ID;

    public GameObject This;

    public GameObject Pan;

    public GameObject CallComplete;
    public GameObject CatComplete;
    public GameObject OnStoveComplete;
    public GameObject OffStoveComplete;
    public GameObject DishesComplete;
    public GameObject BedCompelte;
    public GameObject ComComplete;
    public GameObject FireCompelte;
    public GameObject DoorComplete;
   

    void Start()
    {
        ID = Random.Range(0, 999999);
    }

    public void TaskDone()
    {
        //put here what should happen when the task is done

        This.SetActive(false);

        Debug.Log("TaskIsDone");

        if (name == "PhoneSource")
        {
            keeper.DidPhone = 1;

            tasks.Tasks.Remove("CallMom");
            tasks.callMom.SetActive(true);
            CallComplete.SetActive(true);
        }
        if (name == "CatSource")
        {
            keeper.DidCat = 1;

            tasks.Tasks.Remove("FeedCat");
            tasks.feedCat.SetActive(true);
            CatComplete.SetActive(true);
        }
        if (name == "PanSource")
        {
            float timer = 30;
            if (keeper.DidPan1 == 0)
            {
                keeper.DidPan1 = 1;

                tasks.Tasks.Remove("OnStove");
                tasks.onStove.SetActive(true);
                OnStoveComplete.SetActive(true);

                timer += Time.deltaTime;

                Pan.SetActive(false);
            }
            
            if(timer <= 0)
            {
                Pan.SetActive(true);
                This.SetActive(true);
            }

            if(keeper.DidPan1 == 1)
            {
                keeper.DidPan2 = 1;

                tasks.Tasks.Remove("OffStove");
                tasks.offStove.SetActive(true);
                OffStoveComplete.SetActive(true);
            }
        }
        if (name == "DishesSource")
        {
            keeper.DidDishes = 1;

            tasks.Tasks.Remove("DoDishes");
            tasks.doDishes.SetActive(true);
            DishesComplete.SetActive(true);
        }
        if (name == "BedSource")
        {
            keeper.DidBed = 1;

            tasks.Tasks.Remove("MakeBed");
            tasks.makeBed.SetActive(true);
            BedCompelte.SetActive(true);
        }
        if (name == "ComSource")
        {
            keeper.DidCom = 1;

            tasks.Tasks.Remove("PayBills");
            tasks.payBills.SetActive(true);
            ComComplete.SetActive(true);
        }
        if (name == "FireSource")
        {
            keeper.DidFire = 1;

            tasks.Tasks.Remove("FireAlarm");
            tasks.fireAlarm.SetActive(true);
            FireCompelte.SetActive(true);
        }
        if (name == "DoorSource")
        {
            keeper.DidDoor = 1;

            tasks.Tasks.Remove("BuyPresent");
            tasks.buyPresent.SetActive(true);
            DoorComplete.SetActive(true);
        }
    }


}
