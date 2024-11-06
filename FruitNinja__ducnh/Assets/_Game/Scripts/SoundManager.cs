using System.Collections;
using System.Collections.Generic;
using GlobalEnum;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    private static SoundManager ins;
    public static SoundManager Ins => ins;

    [Header("Audio Source")]
    [SerializeField] private SFXMusic sfxSourcePrefab;
    [SerializeField] private AudioClip[] sfxSources;
    [SerializeField] private AudioSource[] backgroundMusic;
    [SerializeField] private AudioMixerGroup mixerGroup;
    public Camera MainCamera;
    [Header("Audio Clip")]
    [SerializeField] private AudioClip loseSound;
    

    private static EBackgroundMusic currentBgMusic=EBackgroundMusic.MainMenu;
    public bool IsMute;
    
    public override void Awake()
    {
        base.Awake();
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }

    public void Mute(bool isMute){
        this.IsMute=isMute;
        for(int i=0;i<backgroundMusic.Length;i++){
            backgroundMusic[i].mute=isMute;
        }
    }


    public void PlaySFX(ESound eSound)
    {
        SFXMusic sFXMusic = SimplePool.Spawn<SFXMusic>(sfxSourcePrefab);
        sFXMusic.OnInit(mixerGroup);
        AudioSource sfxSource = sFXMusic.SfxSource;
        AudioClip audioClip = null;
        switch (eSound)
        {
            case ESound.Slice:
                
                break;
        }
        if (audioClip == null) return;
        sfxSource.mute=IsMute;
        float clipLength = audioClip.length;
        sFXMusic.OnDespawn(clipLength);
    }

    /// <summary>
    /// Use this when you want to play music when target in view
    /// </summary>
    /// <param name="target"></param>
    /// <param name="eSound"></param>
    public void PlaySFX(Transform target, ESound eSound)
    {
        if (!IsInCameraView(target)) return;
        PlaySFX(eSound);
    }

    public void PlayBackgroundMusic(EBackgroundMusic eBackgroundMusic)
    {
        if(currentBgMusic == eBackgroundMusic) return;
        currentBgMusic=eBackgroundMusic;
        switch (eBackgroundMusic)
        {
            case EBackgroundMusic.InGame:
                backgroundMusic[1].Play();
                backgroundMusic[0].Stop();
                break;
            case EBackgroundMusic.MainMenu:
                backgroundMusic[0].Play();
                backgroundMusic[1].Stop();
                break;
        }
    }




    private bool IsInCameraView(Transform target)
    {
        Vector3 screenPoint = MainCamera.WorldToViewportPoint(target.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}
