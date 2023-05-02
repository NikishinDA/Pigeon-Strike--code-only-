using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollisionController : MonoBehaviour
{
    private Collider _trigger;

    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        gameObject.SetActive(false);
    }
}
