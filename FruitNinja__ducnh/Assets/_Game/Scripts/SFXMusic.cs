using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXMusic : GameUnit
{
    public AudioSource SfxSource;

    public void OnDespawn(float length)
    {
        StartCoroutine(DelayDespawnAudioSource(length));
    }

    IEnumerator DelayDespawnAudioSource(float length)
    {
        yield return new WaitForSeconds(length);
        SimplePool.Despawn(this);
    }

    public void OnInit(AudioMixerGroup audioMixer){
        SfxSource.outputAudioMixerGroup=audioMixer;
    }
}
