using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    public FloatEventSO volumeEvent;
    public VoidEventSO pauseEvent;
    [Header("广播")]
    public FloatEventSO syncVolumeEvent;
    [Header("组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer mixer;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += onFXEvent;
        BGMEvent.OnEventRaised += onBGMEvent;
        volumeEvent.OnEventRaised += onVolumeEvent;
        pauseEvent.OnEventRaised += OnpauseEvent;
    }

    

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= onFXEvent;
        BGMEvent.OnEventRaised -= onBGMEvent;
        volumeEvent.OnEventRaised -= onVolumeEvent;
        pauseEvent.OnEventRaised -= OnpauseEvent;
    }
    private void OnpauseEvent()
    {
        float amount;
        mixer.GetFloat("MasterVolume", out amount);
        syncVolumeEvent.RaiseEvent(amount);
    }

    private void onVolumeEvent(float amount)
    {
        mixer.SetFloat("MasterVolume", amount*100-80);
    }

    private void onBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void onFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
