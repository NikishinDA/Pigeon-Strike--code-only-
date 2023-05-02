using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterController : MonoBehaviour
{
    [SerializeField] private Text moneyCounter;
    private int _moneyCollected;

    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        if (_moneyCollected <= 0) return;
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name,
            PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal) + _moneyCollected);
        PlayerPrefs.Save();
        _moneyCollected = 0;
    }

    private void Start()
    {
        moneyCounter.text = _moneyCollected.ToString();
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        _moneyCollected++;
        moneyCounter.text = _moneyCollected.ToString();
    }
}