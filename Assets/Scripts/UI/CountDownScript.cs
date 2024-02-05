using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownScript : MonoBehaviour
{

    [SerializeField] private float timeRemaining = 120f;
    private TextMeshProUGUI time;

    private void Start()
    {
        time = this.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (timeRemaining > 0)
        {
            displayTime(timeRemaining);
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetHealth(0);
        }
    }

    void displayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
