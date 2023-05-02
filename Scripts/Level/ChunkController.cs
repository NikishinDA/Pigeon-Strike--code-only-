using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private BonusSpawner chunkBonusSpawner;

    public void SpawnBonuses(Vector3[] positions)
    {
        chunkBonusSpawner.SpawnBonuses(positions);
    }
    public void FillChunk(LevelChunk nextChunk)
    {
        Transform fillTransform = Instantiate(nextChunk.Fill, transform).transform;
        fillTransform.eulerAngles = Vector3.up * nextChunk.Rotation;
    }
}
