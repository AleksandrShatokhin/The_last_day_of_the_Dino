using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowEndGame : MonoBehaviour
{
    [SerializeField] Button restartButton, exitButton;
    [SerializeField] Text timer;
    

    void Start()
    {
        timer.text = GameObject.Find("MainUI").GetComponent<MainUI>().ShowTextTimer().text;

        restartButton.onClick.AddListener(ToRestart);
        exitButton.onClick.AddListener(ToExit);
    }

    void ToRestart()
    {
        SceneManager.LoadScene("Level_1");
    }

    void ToExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
