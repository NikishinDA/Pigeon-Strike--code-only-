using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelTemplate", menuName = "Level/Template", order = 1)]
public class LevelTemplate : ScriptableObject
{
    public LevelChunk[] Chunks;
}

[Serializable]
public class LevelChunk
{
    public GameObject Fill;
    public float Rotation;
    public Vector3[] Bonuses;
}
