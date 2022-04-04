using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Slider distanceToAsteroid;
    [SerializeField] private Text timerText;

    private float timeMin;
    private float timeSec;

    void Start()
    {
        
    }

    void Update()
    {
        ShowTimer();
        ShowText();
        ChangeSlider();
    }

    // вывод времени проведенного в игре
    void ShowTimer()
    {
        timeSec += Time.deltaTime;

        Debug.Log(timeMin + " : " + Mathf.Round(timeSec));

        if (Mathf.Round(timeSec) == 60)
        {
            timeMin += 1.0f;
            timeSec = 0.0f;
        }
    }

    // заполняем текст на экране
    void ShowText()
    {
        timerText.text = timeMin + ":" + Mathf.Round(timeSec);
    }

    // меняем бегунок на слайдере
    void ChangeSlider()
    {
        distanceToAsteroid.value -= 1 * Time.deltaTime;
    }

    public void ChangeSlider(int quantity)
    {
        distanceToAsteroid.value += quantity * 5;
    }
}
