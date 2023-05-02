using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject replayScreen;
    private bool _replayShown;
    private void Awake()
    {
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<StartScenePlayEvent>(OnStartScenePlay);
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<StartScenePlayEvent>(OnStartScenePlay);
        EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);

    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {
        if (_replayShown) return;
        gameScreen.SetActive(false);
        replayScreen.SetActive(true);
        _replayShown = true;
    }

    private void OnStartScenePlay(StartScenePlayEvent obj)
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    private void OnGameOver(GameOverEvent obj)
    {        
        replayScreen.SetActive(false);
        if (obj.IsWin)
        {
            winScreen.SetActive(true);
        }
        else
        {
            loseScreen.SetActive(true); 
            gameScreen.SetActive(false);

        }
    }

    private void OnGameStart(GameStartEvent obj)
    {
    }

    void Start()
    {
        startScreen.SetActive(true);
    }
}