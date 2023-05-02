using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinihserEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] preEffect;
    [SerializeField] private ParticleSystem[] postEffect;
    private void Awake()
    {
        EventManager.AddListener<FinisherPlayerReachGroundEvent>(OnPlayerGroundHit);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherPlayerReachGroundEvent>(OnPlayerGroundHit);

    }

    private void OnPlayerGroundHit(FinisherPlayerReachGroundEvent obj)
    {
        foreach (var system in preEffect)
        {
            system.Stop();
        }

        foreach (var system in postEffect)
        {
            system.Play();
        }
    }
}
