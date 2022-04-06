using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton, exitButton;

    void Start()
    {
        startButton.onClick.AddListener(ToStartGame);
        exitButton.onClick.AddListener(ToExit);
    }

    void ToStartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    void ToExit()
    {
        Application.Quit();
    }
}
