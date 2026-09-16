using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO afterSceneLoadedEvent;

    private CinemachineConfiner2D confiner2D;

    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
        

    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OncameraShakeEvent;

        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
        
    }
    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OncameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        StartCoroutine(DelayedGetNewCameraBounds());   //  延迟一帧
    }

    private IEnumerator DelayedGetNewCameraBounds()
    {
        yield return null;
        GetNewCameraBounds();
    }

    private void OncameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    //private void Start()
    //{
    //    GetNewCameraBounds();
    //}

    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();

        confiner2D.InvalidateCache();
    }
}
