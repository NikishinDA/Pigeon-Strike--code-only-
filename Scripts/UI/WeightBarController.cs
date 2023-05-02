using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightBarController : MonoBehaviour
{
    [SerializeField] private Slider weightSlider;

    private void Awake()
    {
        EventManager.AddListener<PlayerWeightChangeEvent>(OnPlayerWeightChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerWeightChangeEvent>(OnPlayerWeightChange);

    }

    private void OnPlayerWeightChange(PlayerWeightChangeEvent obj)
    {
        weightSlider.value = obj.Progress;
    }
}
