using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinNextButtonController : MonoBehaviour
{
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNextButtonClick);
    }

    private void OnNextButtonClick()
    {
        SceneLoader.LoadNextLevel();
    }
}
