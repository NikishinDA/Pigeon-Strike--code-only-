using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    private float _maxHeight = -1;
    private float _desiredProgress;
    private float _currentProgress;
    [SerializeField] private Slider pb;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<PlayerProgressEvent>(OnPlayerProgress);
    }

    private void OnDestroy()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<PlayerProgressEvent>(OnPlayerProgress);
    }

    private void OnPlayerProgress(PlayerProgressEvent obj)
    {
        _desiredProgress = obj.Height;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        _maxHeight = obj.PlayerHeight;
        _currentProgress = _maxHeight;
    }

    private void Update()
    {
        if (_maxHeight > 0)
        {
            _currentProgress = Mathf.Lerp(_currentProgress, _desiredProgress, 2 * Time.deltaTime);
            pb.value = 1f - Mathf.Lerp(0f, 1f, _currentProgress / _maxHeight);
        }
    }
}
