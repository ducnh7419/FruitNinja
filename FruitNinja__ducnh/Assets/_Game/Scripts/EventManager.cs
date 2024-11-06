using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    [SerializeField]private float totalFreezeTime;
    [SerializeField]private float totalFrenzyTime;
    [SerializeField]private float frenzySpawnerSpeed;
    public void OnFreezeEvent(){
        StartCoroutine(FreezeTimne(totalFreezeTime));
    }

    public void OnFrenzyEvent(){
        StartCoroutine(ChangeSpawnerSpeed(totalFreezeTime,frenzySpawnerSpeed));
    }

    private IEnumerator FreezeTimne(float time)
    {
        GameManager.Instance.PlayFrostFx();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(time);
        Debug.Log("Disable");
        Time.timeScale = 1f;
        GameManager.Instance.DisbleFrostFx();
    }

    private IEnumerator ChangeSpawnerSpeed(float time, float spawnerSpeed)
    {
        GameManager.Instance.ChangeSpawnerSpeed(spawnerSpeed);
        yield return new WaitForSeconds(time);
        GameManager.Instance.ChangeSpawnerSpeed(1f);
    }

}
