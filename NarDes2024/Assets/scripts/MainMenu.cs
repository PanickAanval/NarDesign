using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string NextScene;

    public void ClickStart()
    {
        SceneManager.LoadScene(NextScene);
    }
}
