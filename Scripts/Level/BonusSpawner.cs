using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bonusPrefab;
    
    [SerializeField] private float horizontalBorder;
    [SerializeField] private float downBorder;

    public void SpawnBonuses(Vector3[] pos)
    {
        Vector3 spawnPos = new Vector3();
        foreach (var position in pos)
        {
            spawnPos.x = position.x * horizontalBorder;
            spawnPos.y = position.z * -downBorder;
            spawnPos.z = position.y * horizontalBorder;
            Instantiate(bonusPrefab, transform).transform.localPosition = spawnPos;
        }
    }
}
