using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICIngame : UICanvas
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private CountdownTimer countdownTimer;


    public void OnInit(float time)
    {
        scoreTxt.SetText("0");
        countdownTimer.OnInit(time);
    }

    public override void OnNotify()
    {
        base.OnNotify();
        scoreTxt.SetText(ScoreManager.Instance.GetCurrentScore().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
