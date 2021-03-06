using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;


    private AudioSource gameAudio;
    [SerializeField] private AudioClip audioEndGame;

    private const string completeStone = "???????? ??????????, ????? ??????? ?? ?????!";
    private const string needPickupStone = "????? ????? ??????? ?????!";

    [SerializeField] private int counterAsteroid;
    private GameObject triggerThrow, triggerJump, triggerClick;

    [SerializeField] private GameObject mainui;
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private GameObject windowEndGame, windowPause;

    private bool isEndGame;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isEndGame = false;

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
            GetMainUIForChangeSliderAsteroid();
        }
    }

    public void GetMainUIForChangeSliderAsteroid()
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
        MainAudioOff();
        isEndGame = true;
    }

    public void AudioGame(AudioClip audio)
    {
        gameAudio.PlayOneShot(audio, 0.5f);
    }

    // ?????????/?????????? ??????? ??????? ??????
    public void MainAudioOn() => cameraMain.GetComponent<AudioSource>().Play();
    public void MainAudioOff() => cameraMain.GetComponent<AudioSource>().Stop();

    public void PauseModeOn()
    {
        if (!isEndGame)
        {
            Time.timeScale = 0.0f;
            Instantiate(windowPause, transform.position, transform.rotation);
            mainui.SetActive(false);
        }
    }

    public void PauseModeOff()
    {
        if (!isEndGame)
        {
            GameObject pause = GameObject.Find("PauseMode(Clone)");

            Time.timeScale = 1.0f;
            Destroy(pause);
            mainui.SetActive(true);
        }
    }

    public MainUI GetMainUI()
    {
        return mainui.GetComponent<MainUI>();
    }
}
