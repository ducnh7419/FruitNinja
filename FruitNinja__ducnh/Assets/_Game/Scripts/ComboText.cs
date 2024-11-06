using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboText : GameUnit
{
    [SerializeField] private TextMeshPro comboTxt;
    
    public void OnInit(int comboCount){
        comboTxt.SetText(string.Format("x{0} combo",comboCount ));
        StartCoroutine(DelayDespawn());
    }

    public void OnDespawn(){
        SimplePool.Despawn(this);
    }

    IEnumerator DelayDespawn(){
        yield return new WaitForSeconds(0.3f);
        OnDespawn();
    }

}
