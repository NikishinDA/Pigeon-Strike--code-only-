using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    private void Start()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
    }
}
