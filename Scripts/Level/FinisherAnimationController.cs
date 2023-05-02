using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherAnimationController : MonoBehaviour
{
    private Animator _charAnimator;

    private void Awake()
    {
        _charAnimator = GetComponent<Animator>();
        EventManager.AddListener<FinisherPlayerReachGroundEvent>(OnReachGround);
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherPlayerReachGroundEvent>(OnReachGround);
        EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);

    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {
        _charAnimator.SetTrigger("Replay");
    }

    private void OnReachGround(FinisherPlayerReachGroundEvent obj)
    {
        _charAnimator.SetTrigger("Hit");
    }
}
