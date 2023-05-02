using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexColliderController : MonoBehaviour
{
    [SerializeField] private ObstacleCollisionController[] triggers;

    private void Awake()
    {
        foreach (var obstacleCollisionController in triggers)
        {
            obstacleCollisionController.CollisionDetected += ObstacleCollisionControllerOnCollisionDetected;
        }
    }

    private void ObstacleCollisionControllerOnCollisionDetected()
    {
        foreach (var obstacleCollisionController in triggers)
        {
            obstacleCollisionController.SetActive(false);
        }
    }
}
