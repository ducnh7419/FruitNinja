using System.Collections;
using System.Collections.Generic;
using GlobalEnum;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : EventObserver
{
    [SerializeField] private Collider spawnArea;
    private IState<Spawner> currentState;
    private bool isCoolDown;
    [SerializeField] private float bombChance = 0.07f;
    [SerializeField] private float frotstFruitChance = 0.05f;
    [SerializeField] private float rainbowFruitChance = 0.03f;
    public float MinSpawnDelay = 0.25f;
    public float MaxSpawnDelay = 1f;

    public float MinAngle = -15f;
    public float MaxAngle = 15f;

    public float MinForce = 18f;
    public float MaxForce = 22f;

    public float MaxLifeTime = 5f;
    private float spawnerSpeed = 1f;
    private bool isFreeze;
    private bool isFrenzy;

    private void OnEnable(){
        ChangeState(new IngameState());
    }

    private void FixedUpdate()
    {
        currentState?.OnExecute(this);

    }
    private void SpawnFruit(EFruitType eFruitType)
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
        Quaternion rot = Quaternion.Euler(0f, 0f, Random.Range(MinAngle, MaxAngle));
        float force = Random.Range(MinForce, MaxForce);
        switch (eFruitType)
        {
            case EFruitType.Fruit:
                Fruit fruitPrefab = GameManager.Instance.FruitsSO.GetRandomFruits();
                Fruit fruit = SimplePool.Spawn<Fruit>(fruitPrefab, position, rot);
                StartCoroutine(DelayFruitDespawn(fruit));
                fruit.AddForce(fruit.TF.up.normalized * force);
                break;
            case EFruitType.Bomb:
                Bomb bombPrefab = GameManager.Instance.FruitsSO.GetBomb();
                Bomb bomb = SimplePool.Spawn<Bomb>(bombPrefab, position, rot);
                StartCoroutine(DelayBombDespawn(bomb));
                bomb.AddForce(bomb.TF.up.normalized * force);
                break;
            case EFruitType.Frost:
                Fruit frostPrefab = GameManager.Instance.FruitsSO.GetFrostFruit();
                Fruit frostFruit = SimplePool.Spawn<Fruit>(frostPrefab, position, rot);
                StartCoroutine(DelayFruitDespawn(frostFruit));
                frostFruit.AddForce(frostFruit.TF.up.normalized * force);
                break;
            case EFruitType.RainBow:
                Fruit rainbowPrefab = GameManager.Instance.FruitsSO.GetRainbowFruit();
                Fruit rainbowFruit = SimplePool.Spawn<Fruit>(rainbowPrefab, position, rot);
                StartCoroutine(DelayFruitDespawn(rainbowFruit));
                rainbowFruit.AddForce(rainbowFruit.TF.up.normalized * force);
                break;
            case EFruitType.Pomegranate:
                Fruit pomegranatePrefab=GameManager.Instance.FruitsSO.GetPomeGranate();
                Fruit pomegranate=SimplePool.Spawn<Fruit>(pomegranatePrefab, position, rot);
                pomegranate.AddForce(pomegranate.TF.up.normalized*force);
                break;
        }
        isCoolDown = true;
        StartCoroutine(DelaySpawn());
    }

   

    public void InGameSpawn()
    {
        if (isCoolDown) return;
        if (Random.value < rainbowFruitChance)
        {
            SpawnFruit(EFruitType.RainBow);
        }
        else if (Random.value < frotstFruitChance)
        {
            SpawnFruit(EFruitType.Frost);
        }
        else if (Random.value < bombChance)
        {
            SpawnFruit(EFruitType.Bomb);
        }else{
            SpawnFruit(EFruitType.Fruit);
        }
    }

    public void PreEndGameSpawn(){
        SpawnFruit(EFruitType.Pomegranate);
    }


    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(Random.Range(MinSpawnDelay / spawnerSpeed, MaxSpawnDelay / spawnerSpeed));
        isCoolDown = false;
    }

    private IEnumerator DelayFruitDespawn(Fruit fruit)
    {
        yield return new WaitForSeconds(MaxLifeTime);
        fruit.OnDespawn();
    }

    private IEnumerator DelayBombDespawn(Bomb bomb)
    {
        yield return new WaitForSeconds(MaxLifeTime);
        bomb.OnDespawn();
    }

    public void ChangeSpawnerSpeed(float spawnerSpeed)
    {
        this.spawnerSpeed = spawnerSpeed;
    }

    public void ChangeState(IState<Spawner> state)
    {
        currentState?.OnExit(this);
        currentState = state;
        currentState?.OnEnter(this);
    }

    public override void OnFreezeTime()
    {
        base.OnFreezeTime();
        isFreeze=true;
    }

    public override void OnFrenzyTIme()
    {
        base.OnFrenzyTIme();
        isFrenzy=true;
    }
}
