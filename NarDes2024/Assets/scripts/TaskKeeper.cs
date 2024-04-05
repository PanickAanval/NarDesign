using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskKeeper : MonoBehaviour
{
    public static TaskKeeper keeper;

    public float InteractTimerIncreases;

    public float DidPhone;
    public float DidCat;
    public float DidPan1;
    public float DidPan2;
    public float DidDishes;
    public float DidBed;
    public float DidCom;
    public float DidFire;
    public float DidDoor;

    public float PlayerCalledMom;
    public float CatRanAway;
    public float OffStoveGetsRemovedFaster;

    public string NormDay1End;
    public string AltDay1End;
    public string FireEnd;
    public string EvicEnd;
    public string NormDay2End;
    public string NoMomEnd;
    public string NoPresentEnd;
    public string NeutralEnd;
    public string GoodEnd;

     void Awake()
    {

        OnlyKeeper();
       
    }

     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

   
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded:" + scene.name);

       // Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Day2Check")
        {
            if (DidPhone == 0 || DidPan1 == 0 || DidDishes == 0 || DidBed == 0)
            {
                //timer for tasks becomes longer
                InteractTimerIncreases = InteractTimerIncreases + 1;

            }
            if (DidPan2 == 0 || DidCom == 0 || DidCat == 0)
            {
                //load alternative day 1 dialogue (player did something wrong that would otherwise give them a game over)
                SceneManager.LoadScene(AltDay1End);
            }

            if (DidCat == 1 && DidPan2 == 1 && DidCom == 1)
            {
                //load normal day 1 dialogue (player did all tasks that would normally give them a game over)
                SceneManager.LoadScene(NormDay1End);
            }

        }

        if (scene.name == "Day2")
        {
            DidPhone = 0;
            DidCat = 0;
            DidPan1 = 0;
            DidPan2 = 0;
            DidDishes = 0;
            DidBed = 0;
            DidCom = 0;
            DidFire = 0;
            DidDoor = 0;
        }

        if (scene.name == "Day3Check")
        {
            if (DidPhone == 0 || DidPan1 == 0 || DidDishes == 0 || DidBed == 0)
            {
                //timer for tasks becomes longer
                InteractTimerIncreases = InteractTimerIncreases + 1; 
                
            }

            if (DidPan2 == 0)
            {
                //go to fire end
                SceneManager.LoadScene(FireEnd);
            }
            if (DidCom == 0)
            {
                //go to eviciton end
                SceneManager.LoadScene(EvicEnd);
            }
            if (DidFire == 0)
            {
                OffStoveGetsRemovedFaster = 1;
            }
            if (DidCat == 0)
            {
                CatRanAway = 1;
            }

            if (DidPan2 == 1 && DidCom == 1)
            {
                //load normal day 2 dialogue (player did all tasks that would otherwise give them a game over)
                SceneManager.LoadScene(NormDay2End);
            }
        }

        if (scene.name == "Day3")
        {
            DidPhone = 0;
            DidCat = 0;
            DidPan1 = 0;
            DidPan2 = 0;
            DidDishes = 0;
            DidBed = 0;
            DidCom = 0;
            DidFire = 0;
            DidDoor = 0;
        }

        if (scene.name == "Day4Check")
        {

            if (DidPan2 == 0)
            {
                //go to fire end
                SceneManager.LoadScene(FireEnd);
            }

            if (DidCom == 0)
            {
                //go to eviciton end
                SceneManager.LoadScene(EvicEnd);
            }

            if (PlayerCalledMom == 0)
            {
                //go to no mom end
                SceneManager.LoadScene(NoMomEnd);
            }

            if (DidDoor == 0 && PlayerCalledMom >= 1)
            {
                //go to no present end
                SceneManager.LoadScene(NoPresentEnd);
            }

            if (DidPhone == 0 || DidPan1 == 0 || DidDishes == 0 || DidBed == 0 || DidCat == 0)
            {
                //go to neutral end
                SceneManager.LoadScene(NeutralEnd);
            }

            if (DidPhone == 1 && DidPan1 == 1 && DidDishes == 1 && DidBed == 1 && DidCat == 1 && DidPan2 == 1 && DidCom == 1 && DidDoor == 1)
            {
                //go to good end
                SceneManager.LoadScene(GoodEnd);
            }
            Debug.Log("je mama");
        }

        if (scene.name == "MainMenu")
        {
            DidPhone = 0;
            DidCat = 0;
            DidPan1 = 0;
            DidPan2 = 0;
            DidDishes = 0;
            DidBed = 0;
            DidCom = 0;
            DidFire = 0;
            DidDoor = 0;
        }
    }

     void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnlyKeeper()
    {
        if (keeper == null)
        {
            DontDestroyOnLoad(this);
            keeper = this;
        }
        else
        {
            if (keeper != this)
            {
                Destroy(gameObject);
            }
        }
    }



}
