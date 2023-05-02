using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private float normalWeightAddition = 0.5f;
    [SerializeField] private int normalCostAddition = 15;
    [SerializeField] private int[] startCostValues;
    [SerializeField] private float[] startWeightValues;
    private int _upgradeLevel;
    private int _upgradeCost;
    [SerializeField] private Text totalMoneyText;
    [SerializeField] private Text costMoneyText;
    private int _totalMoney;
    [SerializeField] private ParticleSystem pressEffect;

    private void Awake()
    {
        _upgradeLevel = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.UpgradeLevel);
        _upgradeCost = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.UpgradeCost);
        _totalMoney = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.MoneyTotal);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        totalMoneyText.text = _totalMoney.ToString();
        costMoneyText.text = _upgradeCost.ToString();
    }

    private void OnUpgradeButtonClick()
    {
        if (_totalMoney >= _upgradeCost)
        {
            _totalMoney -= _upgradeCost;
            _upgradeLevel++;
            if (_upgradeLevel <= startCostValues.Length && _upgradeLevel <= startWeightValues.Length)
            {
                _upgradeCost = startCostValues[_upgradeLevel - 1];
                PlayerPrefs.SetFloat(PlayerPrefsStrings.MaxWeight.Name,
                    PlayerPrefsStrings.GetFloatValue(PlayerPrefsStrings.MaxWeight) +
                    startWeightValues[_upgradeLevel - 1]);
            }
            else
            {
                _upgradeCost += normalCostAddition;
                PlayerPrefs.SetFloat(PlayerPrefsStrings.MaxWeight.Name,
                    PlayerPrefsStrings.GetFloatValue(PlayerPrefsStrings.MaxWeight) +
                    normalWeightAddition);
            }
            SaveChanges();
            EventManager.Broadcast(GameEventsHandler.UpgradeButtonPressEvent);
            Taptic.Medium();
            pressEffect.Play();
        }
    }

    private void SaveChanges()
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.UpgradeLevel.Name, _upgradeLevel);
        PlayerPrefs.SetInt(PlayerPrefsStrings.UpgradeCost.Name, _upgradeCost);
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal.Name, _totalMoney);
        PlayerPrefs.Save();
        
        totalMoneyText.text = _totalMoney.ToString();
        costMoneyText.text = _upgradeCost.ToString();
    }
}