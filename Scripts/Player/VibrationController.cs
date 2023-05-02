using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<PlayerBoostEvent>(OnPlayerBoost);
        EventManager.AddListener<PlayerCollectibleEvent>(OnCollectibleEvent);
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<FinisherPlayerReachGroundEvent>(OnReachGround);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<PlayerBoostEvent>(OnPlayerBoost);
        EventManager.RemoveListener<PlayerCollectibleEvent>(OnCollectibleEvent);
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.RemoveListener<FinisherPlayerReachGroundEvent>(OnReachGround);
    }

    private void OnReachGround(FinisherPlayerReachGroundEvent obj)
    {
        Taptic.Success();
    }

    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        Taptic.Failure();
    }

    private void OnCollectibleEvent(PlayerCollectibleEvent obj)
    {
        Taptic.Light();
    }

    private void OnPlayerBoost(PlayerBoostEvent obj)
    {
        Taptic.Medium();
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        Taptic.Light();
    }
}
