using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] finisherPrefabs;
    [SerializeField] private Transform spawnPoint;
    private int _level;

    private void Awake()
    {
        _level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
    }

    private void Start()
    {
        Instantiate(finisherPrefabs[_level % finisherPrefabs.Length], transform);
    }
}
