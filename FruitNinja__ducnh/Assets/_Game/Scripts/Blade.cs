using System;
using System.Collections;
using System.Collections.Generic;
using GlobalEnum;
using UnityEditor.UIElements;
using UnityEngine;

public class Blade : Subject
{

    [SerializeField] private Collider bladeCollider;
    [SerializeField] private float sliceForce = 3f;
    private int comboCount = 0;
    private float comboTimer = 0f;
    public float ComboTimeWindow = 0.5f;
    public Vector3 Direction { get; set; }
    private bool onCombo;
    private bool isFreeze;
    private bool isFrenzy;

    private void Update()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
    }

    public void MoveBlade(Vector3 pos)
    {
        TF.position = pos;
    }

    public void OnInit()
    {
        Direction = Vector3.zero;
        ResetCombo();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Bomb":
                OnBombSliced(other);
                break;
            case "Fruit":
                Debug.Log("Slice");
                OnCommonFruitSliced(other);
                break;
            case "Frost":
                OnFrostFruitSliced(other);
                break;
            case "Rainbow":
                OnRainbowFruitSliced(other, 2f);
                break;
            case "Pomegranate":
                OnPomegranateSliced();
                break;
        }
        DoThing(EEvent.ChangeScore);
    }

    private void StartCombo()
    {
        comboCount = 1;
        comboTimer = ComboTimeWindow;
        onCombo = true;
    }

    private void IncreaseCombo()
    {
        comboCount++;
        comboTimer -= Time.deltaTime;
    }

    private void ResetCombo()
    {
        onCombo = false;
        comboCount = 0;
        comboTimer = 0;
    }

    private void CheckCombo()
    {
        if (!onCombo)
        {
            StartCombo();
        }
        else
        {
            if (comboTimer > 0)
            {
                IncreaseCombo();
                Debug.Log(comboTimer);
            }
            else
            {
                PlayComboVFX(TF.position);
                ResetCombo();
            }
        }
    }

    private void PlayComboVFX(Vector3 pos)
    {
        ComboText comboText = SimplePool.Spawn<ComboText>(PoolType.ComboText, new Vector3(pos.x, pos.y + 3f, pos.z + 1f), Quaternion.identity);
        comboText.OnInit(comboCount);
    }

    private void OnBombSliced(Collider other)
    {
        ScoreManager.Instance.DecreaseScore();
        Bomb bomb = other.GetComponent<Bomb>();
        bomb.Slice();
        ResetCombo();
    }

    private void OnCommonFruitSliced(Collider other)
    {
        Fruit fruit = other.GetComponent<Fruit>();
        fruit.Slice(Direction, TF.position, sliceForce);
        ScoreManager.Instance.IncreseScore(comboCount);
        CheckCombo();
    }

    private void OnFrostFruitSliced(Collider other)
    {
        Fruit frostFruit = other.GetComponent<Fruit>();
        frostFruit.Slice(Direction, TF.position, sliceForce);
        EventManager.Instance.OnFreezeEvent();
        ScoreManager.Instance.IncreseScore(comboCount);
        CheckCombo();
        DoThing(EEvent.FreezeTime);
    }

    
    private void OnRainbowFruitSliced(Collider other, float spawnerSpeed)
    {
        Fruit rainbowFruit = other.GetComponent<Fruit>();
        rainbowFruit.Slice(Direction, TF.position, sliceForce);
        EventManager.Instance.OnFrenzyEvent();
        ScoreManager.Instance.IncreseScore(comboCount);
        CheckCombo();
        DoThing(EEvent.FrenzyTime);
    }

    

    private void OnPomegranateSliced()
    {

    }
}
