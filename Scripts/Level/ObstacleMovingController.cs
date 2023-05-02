using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovingController : MonoBehaviour
{
    [SerializeField] private Transform movingTransform;
    [SerializeField] private Vector2 movingConstraints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int direction;
    private Vector3 _moveVector;

    private void Awake()
    {
        direction = direction >= 0 ? 1 : -1;
    }

    private void Update()
    {
        _moveVector = movingTransform.localPosition;
        _moveVector.x += Time.deltaTime * direction * moveSpeed;
        if (_moveVector.x <= movingConstraints.x)
            direction = 1;
        else if (_moveVector.x >= movingConstraints.y)
            direction = -1;
        movingTransform.localPosition = _moveVector;
        
    }
}