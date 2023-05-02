using System;
using System.Collections;
using System.Collections.Generic;
using ch.sycoforge.Decal.Wrapper;
using UnityEngine;

public class ObstacleCollisionController : MonoBehaviour
{
    [SerializeField] private bool isLethal;
    private Collider _trigger;
    public event Action CollisionDetected;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    public void SetActive(bool toggle)
    {
        _trigger.enabled = toggle;
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        var evt = GameEventsHandler.PlayerObstacleHitEvent;
        evt.IsLethal = isLethal;
        EventManager.Broadcast(evt);
        CollisionDetected?.Invoke();
    }
}