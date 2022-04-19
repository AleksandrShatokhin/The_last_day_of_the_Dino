using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;


    private AudioSource gameAudio;
    [SerializeField] private AudioClip audioEndGame;

    private const string completeStone = "Обломков достаточно, можно прыгать на рычаг!";
    private const string needPickupStone = "Нужно снова собрать камни!";

    [SerializeField] private int counterAsteroid;
    private GameObject triggerThrow, triggerJump, triggerClick;

    [SerializeField] private GameObject mainui;
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject windowEndGame;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;

        gameAudio = GetComponent<AudioSource>();

        counterAsteroid = 0;

        triggerThrow = GameObject.Find("TriggerForTheThrow");
        triggerJump = GameObject.Find("TriggerForTheJump");
        triggerClick = GameObject.Find("DeviceForBlowing");
        triggerJump.GetComponent<BoxCollider>().enabled = false;
        triggerClick.GetComponent<BoxCollider>().enabled = false;
    }

    public void Counter(int quantity)
    {
        counterAsteroid += quantity;
        mainui.GetComponent<MainUI>().CounterAsteroidInVolcano(counterAsteroid);

        if (counterAsteroid == 5)
        {
            ShowInfoOnScreen(completeStone);

            triggerThrow.GetComponent<BoxCollider>().enabled = false;
            triggerJump.GetComponent<BoxCollider>().enabled = true;
            triggerClick.GetComponent<BoxCollider>().enabled = true;
        }

        if (counterAsteroid == 0)
        {
            ShowInfoOnScreen(needPickupStone);

            triggerThrow.GetComponent<BoxCollider>().enabled = true;
            triggerJump.GetComponent<BoxCollider>().enabled = false;
            triggerClick.GetComponent<BoxCollider>().enabled = false;
            GetMainUI();
        }
    }

    public void GetMainUI()
    {
        mainui.GetComponent<MainUI>().ChangeSliderAsteroid(5);
    }

    public float CheckFeelingFull()
    {
        float state = mainui.GetComponent<MainUI>().CheckFeelingFull();
        return state;
    }

    public void AddFeelingFull()
    {
        mainui.GetComponent<MainUI>().ChangeSliderFeelingFull(5);
    }

    public void GetShakeCamera()
    {
        cameraMain.GetComponent<CameraController>().ShakeCamera();
    }

    public void ShowInfoOnScreen(string info)
    {
        mainui.GetComponent<MainUI>().ShowInfoText(info);
    }

    public void EndGame()
    {
        Instantiate(windowEndGame, transform.position, transform.rotation);
        AudioGame(audioEndGame);
        Time.timeScale = 0.0f;
    }

    public void AudioGame(AudioClip audio)
    {
        gameAudio.PlayOneShot(audio, 0.5f);
    }
}
