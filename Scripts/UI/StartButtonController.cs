using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        EventManager.Broadcast(GameEventsHandler.StartScenePlayEvent);
    }
}
