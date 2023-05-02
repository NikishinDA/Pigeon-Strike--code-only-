using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAmbianceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ambiances;
    [SerializeField] private int levelsPerEnv;

    private int _level;

    private void Awake()
    {
        _level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
    }
    private void Start()
    {
        Instantiate(ambiances[_level / levelsPerEnv % ambiances.Length]);
    }
}
