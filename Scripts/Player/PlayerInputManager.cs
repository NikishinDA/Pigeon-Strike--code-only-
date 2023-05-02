using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _newTouchViewport;
    private Vector2 _oldTouchViewport;

    public Vector2 TouchDelta { get; private set; }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    /*public float GetTouchDelta()
    {
        float delta = _newTouchViewportX - _oldTouchViewportX;
        _oldTouchViewportX = _newTouchViewportX;
        return delta;
    }*/
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldTouchViewport = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
             _newTouchViewport = _mainCamera.ScreenToViewportPoint(Input.mousePosition);
             TouchDelta = (_newTouchViewport - _oldTouchViewport);//  Time.deltaTime;
             _oldTouchViewport = _newTouchViewport;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchDelta = Vector2.zero;
        }
    }
}
