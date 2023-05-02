using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplayScreenController : MonoBehaviour
{
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    private void OnNextButtonClick()
    {
        var evt = GameEventsHandler.GameOverEvent;
        evt.IsWin = true;
        EventManager.Broadcast(evt);
    }
}
