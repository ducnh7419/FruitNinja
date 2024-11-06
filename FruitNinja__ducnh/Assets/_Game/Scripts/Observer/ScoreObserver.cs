using UnityEngine;
using System;

public class ScoreObserver : MonoBehaviour,IObserver
{

    public virtual void OnNotify()
    {
    }

    private void Awake()
    {
        Subject.ScoreUpdate += OnNotify;
    }
    private void OnDestroy()
    {
        Subject.ScoreUpdate -= OnNotify;
    }
}