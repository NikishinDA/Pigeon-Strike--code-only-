using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCollisionController : MonoBehaviour
{
    [SerializeField] private GameObject view;
    [SerializeField] private ParticleSystem effect;
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerBoostEvent);
        _trigger.enabled = false;
        effect.Play();
        
        view.SetActive(false);
    }
}
