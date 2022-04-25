using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowPause : MonoBehaviour
{
    [SerializeField] private Button continueButton, exitButton;

    void Start()
    {
        continueButton.onClick.AddListener(ToContinue);
        exitButton.onClick.AddListener(ToExit);
    }

    void ToContinue()
    {
        GameController.GetInstance().PauseModeOff();
    }

    void ToExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
