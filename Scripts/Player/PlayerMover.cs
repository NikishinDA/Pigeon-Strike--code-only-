using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private Transform playerView;
    [SerializeField] private float fallSpeed;
    [SerializeField] private Vector2 strafeSpeed;
    [SerializeField] private Vector2 strafeConstraints;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float deceleration = 1f;
    private Vector3 _moveVector;
    private Vector2 _strafeVector;
    private float _speedModifier = 1f;
    private bool _isActive;
    private bool _isMoveDisabled = false;
    private void Awake()
    {
        EventManager.AddListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.AddListener<PlayerBoostEvent>(OnPlayerBoostEvent);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
        EventManager.AddListener<FinisherPlayerReachGroundEvent>(OnPlayerReachGround);
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnObstacleHit);
        EventManager.RemoveListener<PlayerBoostEvent>(OnPlayerBoostEvent);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);

        EventManager.RemoveListener<FinisherPlayerReachGroundEvent>(OnPlayerReachGround);
        EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        if (!obj.IsWin)
            _isActive = false;
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        _isMoveDisabled = true;
    }

    private void OnPlayerReachGround(FinisherPlayerReachGroundEvent obj)
    {
        _isActive = false;
    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {
        _isActive = true;
        playerView.localPosition = Vector3.zero;
    }

    private void OnDebugCall(DebugCallEvent obj)
    {
        fallSpeed = -obj.Speed;
        strafeSpeed.x = obj.Strafe;
        strafeSpeed.y = obj.Strafe;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _isActive = true;
        InvokeRepeating(nameof(BroadcastDistanceMessage), 0, 0.2f);
    }

    private void OnPlayerBoostEvent(PlayerBoostEvent obj)
    {
        _speedModifier = boostSpeed;
    }

    private void OnObstacleHit(PlayerObstacleHitEvent obj)
    {
        
            _speedModifier = slowSpeed;
        
    }

    private void Update()
    {
        if (!_isActive) return;
        _moveVector = transform.position;
        if (transform.position.y > 0)
            _moveVector.y += fallSpeed * _speedModifier * Time.deltaTime;
        else
        {
            /*var evt = GameEventsHandler.GameOverEvent;
            evt.IsWin = true;
            EventManager.Broadcast(evt);*/
            _isActive = false;
        }

        transform.position = _moveVector;
        if (_isMoveDisabled) return;
        _moveVector = playerView.localPosition;
        _strafeVector = inputManager.TouchDelta * strafeSpeed;
        _moveVector.x = Mathf.Clamp(_moveVector.x + _strafeVector.x, -strafeConstraints.x, strafeConstraints.x);
        _moveVector.z = Mathf.Clamp(_moveVector.z + _strafeVector.y, -strafeConstraints.y, strafeConstraints.y);
        playerView.localPosition = _moveVector;
        if (_speedModifier > 1f)
        {
            _speedModifier = Mathf.MoveTowards(_speedModifier, 1f, deceleration * Time.deltaTime);
        }
        else if (_speedModifier < 1f)
        {
            _speedModifier = Mathf.MoveTowards(_speedModifier, 1f, acceleration * Time.deltaTime);

        }
    }

    private void BroadcastDistanceMessage()
    {
        var evt = GameEventsHandler.PlayerProgressEvent;
        evt.Height = transform.position.y;
        EventManager.Broadcast(evt);
    }
}
