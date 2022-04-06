using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Slider distanceToAsteroid;
    [SerializeField] private Text timerText;
    [SerializeField] private Text infoText;
    [SerializeField] private Text textAsteroidInVolcano;
    private int asteroidInVolcano = 0;

    private bool isEndgame = false;

    private float timeMin;
    private float timeSec;

    void Start()
    {
        infoText.text = null;
        textAsteroidInVolcano.text = asteroidInVolcano.ToString();
    }

    void Update()
    {
        CounterTimer();
        ShowTextTimer();
        ChangeSlider();
    }

    // вывод времени проведенного в игре
    void CounterTimer()
    {
        timeSec += Time.deltaTime;

        if (Mathf.Round(timeSec) == 60)
        {
            timeMin += 1.0f;
            timeSec = 0.0f;
        }
    }

    // заполняем текст на экране
    public Text ShowTextTimer()
    {
        timerText.text = timeMin + ":" + Mathf.Round(timeSec);

        return timerText;
    }

    // меняем бегунок на слайдере
    void ChangeSlider()
    {
        distanceToAsteroid.value -= 1 * Time.deltaTime;

        if (distanceToAsteroid.value == 0 && !isEndgame)
        {
            isEndgame = true;
            GameController.GetInstance().EndGame();
        }
    }

    public void ChangeSlider(int quantity)
    {
        distanceToAsteroid.value += quantity * 8;
    }

    IEnumerator ClearInfoText()
    {
        yield return new WaitForSeconds(2.5f);

        infoText.text = null;
    }

    public void ShowInfoText(string info)
    {
        infoText.text = info;
        StartCoroutine(ClearInfoText());
    }

    public Text CounterAsteroidInVolcano(int quantity)
    {
        asteroidInVolcano = quantity;
        textAsteroidInVolcano.text = asteroidInVolcano.ToString();

        return textAsteroidInVolcano;
    }
}
