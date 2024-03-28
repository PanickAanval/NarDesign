using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float TimeLeft = 240;

  //  public string NextScene;

    public TextMeshProUGUI TimerText;

    private void Start()
    {
        TimerText.text = "240";
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
            TimeLeft = 0;
            TimerText.text = "0";
            //go to next scene
        }
    }

    void UpdateTimer(float CurrentTime)
    {
        CurrentTime += 1;

        float minutes = Mathf.FloorToInt(CurrentTime / 60);
        float seconds = Mathf.FloorToInt(CurrentTime % 60);


        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
