using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController GetInstance() => instance;


    [SerializeField] private int counterAsteroid;
    private GameObject triggerThrow, triggerJump, triggerClick;

    [SerializeField] private GameObject mainui;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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

        if (counterAsteroid == 5)
        {
            Debug.Log("Загружено достаточно камней!");

            triggerThrow.GetComponent<BoxCollider>().enabled = false;
            triggerJump.GetComponent<BoxCollider>().enabled = true;
            triggerClick.GetComponent<BoxCollider>().enabled = true;
        }

        if (counterAsteroid == 0)
        {
            Debug.Log("Нужно снова собрать камни!");

            triggerThrow.GetComponent<BoxCollider>().enabled = true;
            triggerJump.GetComponent<BoxCollider>().enabled = false;
            triggerClick.GetComponent<BoxCollider>().enabled = false;
            GetMainUI();
        }
    }

    public void GetMainUI()
    {
        mainui.GetComponent<MainUI>().ChangeSlider(5);
    }
}
