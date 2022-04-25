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

        // ����������� ������� ���� ����������� ������� �������
        currentColor = colorFillSilder.color;
    }

    void Update()
    {
        CounterTimer();
        ShowTextTimer();
        ChangeSliderAsteroid();
    }

    // ����� ������� ������������ � ����
    void CounterTimer()
    {
        timeSec += Time.deltaTime;

        if (Mathf.Round(timeSec) == 60)
        {
            timeMin += 1.0f;
            timeSec = 0.0f;
        }
    }

    // ��������� ����� �� ������
    public Text ShowTextTimer()
    {
        timerText.text = string.Format("{0:00}:{1:00}", timeMin, timeSec);

        return timerText;
    }

    // ������ ������� �� �������� �������� �� ���������
    void ChangeSliderAsteroid()
    {
        // ������� �� ���������
        distanceToAsteroid.value -= 1 * Time.deltaTime;

        if (distanceToAsteroid.value == 0 && !isEndgame)
        {
            isEndgame = true;
            GameController.GetInstance().EndGame();
        }

        // ������� �� �������
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

    // ��� �������� ��������� � �������� ���������
    public void ChangeSliderAsteroid(int quantity)
    {
        distanceToAsteroid.value += quantity * 9;
    }

    // ��� ������������� ��������� �������� �������
    public float CheckFeelingFull()
    {
        return feelingFull.value;
    }

    // ��������� � �������, ����� ���� �������
    public void ChangeSliderFeelingFull(int quantity)
    {
        feelingFull.value += quantity * 4;
    }

    // �������� ����� ����� �����
    IEnumerator ClearInfoText()
    {
        yield return new WaitForSeconds(2.5f);

        infoText.text = null;
    }

    // ����� ����� ������ ������������ �� �������� ������
    public void ShowInfoText(string info)
    {
        infoText.text = info;
        StartCoroutine(ClearInfoText());
    }

    // �������� � ���� ������ ���������� ���������
    public Text CounterAsteroidInVolcano(int quantity)
    {
        asteroidInVolcano = quantity;
        textAsteroidInVolcano.text = asteroidInVolcano.ToString();

        return textAsteroidInVolcano;
    }
}
