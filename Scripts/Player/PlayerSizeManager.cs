using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerSizeManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float deltaScale;
    [SerializeField] private float scaleEffect;
    [SerializeField] private float scaleTime;
    [SerializeField] private Vector2 scaleConstraints;
    private Vector3 _startSize;
    private Sequence _sizeBumpSequence;

    private float _currentScale = 1f;
    private float _oldSize = 1f;
    [SerializeField] private float hitScale;

    private void Awake()
    {
        _startSize = playerTransform.localScale;
        EventManager.AddListener<PlayerCollectibleEvent>(OnPlayerCollectible);
        EventManager.AddListener<PlayerObstacleHitEvent>(OnPlayerObstacleHit);
        EventManager.AddListener<DebugCallEvent>(OnDebugCall);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<StartScenePlayEvent>(OnStartScenePlay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerCollectibleEvent>(OnPlayerCollectible);
        EventManager.RemoveListener<PlayerObstacleHitEvent>(OnPlayerObstacleHit);
        EventManager.RemoveListener<DebugCallEvent>(OnDebugCall);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<StartScenePlayEvent>(OnStartScenePlay);


    }

    private void OnStartScenePlay(StartScenePlayEvent obj)
    {
        scaleConstraints.y = PlayerPrefsStrings.GetFloatValue(PlayerPrefsStrings.MaxWeight);
        if (GetCurrentScale01() < 0.33f)
        {
            _currentScale = (1f / 3f) * (scaleConstraints.y - scaleConstraints.x) - scaleConstraints.x;
            ChangeSize(_currentScale);
        }
    }

    private void OnGameStart(GameStartEvent obj)
    {        
        BroadcastWeightChangeEvent();
    }


    private void OnDebugCall(DebugCallEvent obj)
    {
        scaleConstraints.x = obj.MinSize;
        scaleConstraints.y = obj.MaxSize;
        deltaScale = obj.DeltaSize;
    }

    private void OnPlayerObstacleHit(PlayerObstacleHitEvent obj)
    {
        _currentScale = Mathf.Clamp(_currentScale - hitScale * deltaScale, scaleConstraints.x, scaleConstraints.y);
        ChangeSize(_currentScale);
    }

    private void OnPlayerCollectible(PlayerCollectibleEvent obj)
    {
        _currentScale = Mathf.Clamp(_currentScale + deltaScale, scaleConstraints.x, scaleConstraints.y);
        ChangeSize(_currentScale);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentScale += deltaScale;
            ChangeSize(_currentScale);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentScale -= deltaScale;
            ChangeSize(_currentScale);
        }
#endif
    }

    private void ChangeSize(float size)
    {
        _sizeBumpSequence = DOTween.Sequence();
        _sizeBumpSequence.Append(
            playerTransform.DOScale(_startSize * (size + Mathf.Sign(size - _oldSize) * scaleEffect), scaleTime));
        _sizeBumpSequence.Append(playerTransform.DOScale(_startSize * size, scaleTime));
        _sizeBumpSequence.Play();
        _oldSize = size;
        BroadcastWeightChangeEvent();
    }

    private void BroadcastWeightChangeEvent()
    {
        var evt = GameEventsHandler.PlayerWeightChangeEvent;
        evt.Progress = GetCurrentScale01();
        EventManager.Broadcast(evt);
    }

    public float GetCurrentScale01()
    {
        return Mathf.Clamp01((_currentScale - scaleConstraints.x) / (scaleConstraints.y - scaleConstraints.x));
    }

    public float GetRelativeScale(float rel)
    {
        return (_currentScale - scaleConstraints.x) / (rel - scaleConstraints.x);

    }
}