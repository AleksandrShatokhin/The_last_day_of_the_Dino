using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private PlayerController player_controller;

    [SerializeField] private Slider distanceToAsteroid, feelingFull;

    [SerializeField] private Image colorFillSilder;
    private Color currentColor;

    [SerializeField] private Text timerText;
    [SerializeField] private Text infoText;
    [SerializeField] private Text textAsteroidInVolcano;
    private int asteroidInVolcano = 0;

    private bool isEndgame = false;

    private float timeMin;
    private float timeSec;

    void Start()
    {
        player_controller = GameObject.Find("Player").GetComponent<PlayerController>();

        infoText.text = null;
        textAsteroidInVolcano.text = asteroidInVolcano.ToString();

        // зафиксируем текущий цвет заполненого бегунка сытости
        currentColor = colorFillSilder.color;
    }

    void Update()
    {
        CounterTimer();
        ShowTextTimer();
        ChangeSliderAsteroid();
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
        timerText.text = string.Format("{0:00}:{1:00}", timeMin, timeSec);

        return timerText;
    }

    // меняем бегунок на слайдере дстанции до астероида
    void ChangeSliderAsteroid()
    {
        // бегунок по астероиду
        distanceToAsteroid.value -= 1 * Time.deltaTime;

        if (distanceToAsteroid.value == 0 && !isEndgame)
        {
            isEndgame = true;
            GameController.GetInstance().EndGame();
        }

        // бегунок по сытости
        feelingFull.value -= 1 * Time.deltaTime;

        if (CheckFeelingFull() >= 25)
        {
            colorFillSilder.color = currentColor;
        }
        else
        {
            colorFillSilder.color = Color.red;
        }

    }

    // при выстреле добавляем к слайдеру дистанцию
    public void ChangeSliderAsteroid(int quantity)
    {
        distanceToAsteroid.value += quantity * 9;
    }

    // при необходимости проверить значение сытости
    public float CheckFeelingFull()
    {
        return feelingFull.value;
    }

    // добавляем к сытости, когда едим ящерицу
    public void ChangeSliderFeelingFull(int quantity)
    {
        feelingFull.value += quantity * 4;
    }

    // обнуляем текст через время
    IEnumerator ClearInfoText()
    {
        yield return new WaitForSeconds(2.5f);

        infoText.text = null;
    }

    // общий метод вывода необходимого по ситуации текста
    public void ShowInfoText(string info)
    {
        infoText.text = info;
        StartCoroutine(ClearInfoText());
    }

    // значение в углу экрана необходимо обновлять
    public Text CounterAsteroidInVolcano(int quantity)
    {
        asteroidInVolcano = quantity;
        textAsteroidInVolcano.text = asteroidInVolcano.ToString();

        return textAsteroidInVolcano;
    }
}
