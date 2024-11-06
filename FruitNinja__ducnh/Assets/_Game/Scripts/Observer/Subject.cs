using UnityEngine;
using System;
using GlobalEnum;
public class Subject : GameUnit
{
    public static event Action ScoreUpdate;
    public static event Action FreezeTimeAction;
    public static event Action FrenzyTimeAction;
    public void DoThing(EEvent eEvent)
    {
        switch (eEvent)
        {
            case EEvent.ChangeScore:
                ScoreUpdate?.Invoke();
                break;
            case EEvent.FreezeTime:
                FreezeTimeAction?.Invoke();
                break;
            case EEvent.FrenzyTime:
                FrenzyTimeAction?.Invoke();
                break;
        }
    }
}
