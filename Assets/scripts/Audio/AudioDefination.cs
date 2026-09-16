using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEventSO PlayAudioEvent;

    public AudioClip audioClip;

    public bool PlayOnEnable;

    private void OnEnable()
    {
        if (PlayOnEnable)
        {
            PlayAudioClip(); 
        }
    }
    public void PlayAudioClip()
    {
        PlayAudioEvent.RaiseEvent(audioClip);
    }
}
