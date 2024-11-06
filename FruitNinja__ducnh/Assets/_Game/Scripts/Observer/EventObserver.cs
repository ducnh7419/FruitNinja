using UnityEngine;
using System;

public class EventObserver : MonoBehaviour,IObserver
{

    public virtual void OnNotify()
    {
    }

    public virtual void OnFreezeTime(){

    }

    public virtual void OnFrenzyTIme(){

    }

    private void Awake()
    {
        Subject.FreezeTimeAction += OnFreezeTime;
        Subject.FrenzyTimeAction+=OnFrenzyTIme;

    }
    private void OnDestroy()
    {
        Subject.FreezeTimeAction -= OnFreezeTime;
        Subject.FrenzyTimeAction-=OnFrenzyTIme;
    }


}