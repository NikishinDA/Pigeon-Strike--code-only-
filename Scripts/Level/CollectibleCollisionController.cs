using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.PlayerCollectibleEvent);
        gameObject.SetActive(false);
    }
}
