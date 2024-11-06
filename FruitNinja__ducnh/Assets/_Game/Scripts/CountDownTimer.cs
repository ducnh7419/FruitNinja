using System;
using GlobalEnum;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 90f; //Set the total time for the countdown
    public TextMeshProUGUI timerText;

    public void OnInit(float time)
    {
        totalTime = time;
    }

    void Update()
    {
        if (totalTime > 0)
        {
            // Subtract elapsed time every frame
            totalTime -= Time.deltaTime;

            // Divide the time by 60
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);

            // Set the text string
            timerText.SetText("{0}", minutes * 60 + seconds);
        }
        else
        {
            Debug.Log("C");
            totalTime = 0;
            GameManager.Instance.ChangeState(EState.PreEndGame);
            timerText.SetText("0");
        }
    }
}