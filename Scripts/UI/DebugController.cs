using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    [SerializeField] private InputField speedInput;
    [SerializeField] private InputField strafeInput;
    [SerializeField] private InputField minSizeInput;
    [SerializeField] private InputField maxSizeInput;
    [SerializeField] private InputField delaSizeInput;
    [SerializeField] private Button startButton;
    private float _speed;
    private float _strafe;
    private float _minSize;
    private float _maxSize;
    private float _deltaSize;
    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        _speed = PlayerPrefs.GetFloat("DebugSpeed", 40);
        _strafe = PlayerPrefs.GetFloat("DebugStrafe", 25);
        _minSize = PlayerPrefs.GetFloat("DebugMinSize", 0.1f);
        _maxSize = PlayerPrefs.GetFloat("DebugMaxSize", 3);
        _deltaSize = PlayerPrefs.GetFloat("DeltaSize", 0.15f);
        speedInput.text = _speed.ToString();
        strafeInput.text = _strafe.ToString();
        minSizeInput.text = _minSize.ToString();
        maxSizeInput.text =  _maxSize.ToString();
        delaSizeInput.text = _deltaSize.ToString();
    }

    private void OnStartButtonClick()
    {
        var evt = GameEventsHandler.DebugCallEvent;
        Single.TryParse(speedInput.text, out _speed);
        Single.TryParse(strafeInput.text, out _strafe);
        Single.TryParse(minSizeInput.text, out _minSize);
        Single.TryParse(maxSizeInput.text, out _maxSize);
        Single.TryParse(delaSizeInput.text, out _deltaSize);
        evt.Speed = _speed;
        evt.Strafe = _strafe;
        evt.MinSize = _minSize;
        evt.MaxSize = _maxSize;
        evt.DeltaSize = _deltaSize;
        PlayerPrefs.SetFloat("DebugSpeed", _speed);
        PlayerPrefs.SetFloat("DebugStrafe", _strafe);
        PlayerPrefs.SetFloat("DebugMinSize", _minSize);
        PlayerPrefs.SetFloat("DebugMaxSize", _maxSize);
        PlayerPrefs.SetFloat("DeltaSize", _deltaSize);
        PlayerPrefs.Save();
        EventManager.Broadcast(evt);
    }
}
