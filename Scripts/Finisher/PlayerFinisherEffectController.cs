using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinisherEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem currentParticleSystem;

    public ParticleSystem ParticleSystem => currentParticleSystem;

    private void Awake()
    {
        EventManager.AddListener<FinisherReplayEvent>(OnReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherReplayEvent>(OnReplay);
    }

    private void OnReplay(FinisherReplayEvent obj)
    {
        Destroy(gameObject);
    }
}
