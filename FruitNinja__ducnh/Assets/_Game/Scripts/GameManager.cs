using System;
using System.Collections;
using System.Collections.Generic;
using GlobalEnum;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Camera M_Camera;
    public FruitsSO FruitsSO;
    public float totalTime;
    public FrostEffectFeature frostEffectFeature;
    [SerializeField] private Spawner spawner;
    private EState currState;


    public override void Awake()
    {
        base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        //userData?.OnInitData();

        ChangeState(EState.InGame);


    }

    public void ChangeState(EState state)
    {
        switch (state)
        {
            case EState.StartGame:
                currState = EState.StartGame;
                OnStartGameState();
                break;
            case EState.InGame:
                currState = EState.InGame;
                OnInGameState();
                break;
            case EState.PreEndGame:
                currState=EState.PreEndGame;
                OnPreEndGameState();
                break;
        }
    }

    private void OnPreEndGameState()
    {
        spawner.ChangeState(new PreEndGameState());
    }

    public bool CheckCurrState(EState state){
        if(currState==state){
            return true;
        }
        return false;
    }

    private void OnInGameState()
    {
        Debug.Log("InGame");
        UIManager.Instance.OpenUI<UICIngame>().OnInit(totalTime);
    }

    public void OnStartGameState()
    {

    }

    public void PlayFrostFx()
    {
        frostEffectFeature.SetEnableFeature(true);
    }

    public void DisbleFrostFx()
    {

        frostEffectFeature.SetEnableFeature(false);
        Debug.Log("A");
    }

    public void SetActiveSpawner(bool active)
    {
        spawner.enabled=active;
    }

    public void ChangeSpawnerSpeed(float spawnerSpeed){
        spawner.ChangeSpawnerSpeed(spawnerSpeed);
    }



}
