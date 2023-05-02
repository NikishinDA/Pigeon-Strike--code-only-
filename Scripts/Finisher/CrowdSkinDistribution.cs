using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdSkinDistribution : MonoBehaviour
{
    private int _num;

    private void Awake()
    {
        _num = Random.Range(0, 32);
    }

    public int GetNumber(int mod)
    {
        _num++;
        return _num % mod;
    }
}
