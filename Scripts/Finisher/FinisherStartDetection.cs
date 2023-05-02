using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherStartDetection : MonoBehaviour
{
    private Collider _trigger;
    [SerializeField] private GameObject finisherTopCamera;
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.FinisherStartEvent;
        evt.PlayerPosition = other.transform.position;
        EventManager.Broadcast(evt);
        _trigger.enabled = false;
        finisherTopCamera.SetActive(true);
    }
}
