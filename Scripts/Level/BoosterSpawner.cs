using UnityEngine;
using Random = UnityEngine.Random;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject boostPrefab;
    [SerializeField] private Vector3[] spawnPoints;
    [SerializeField] private float coinWeight;
    [SerializeField] private float boostWeight;
    [SerializeField] private float emptyWeight;

    private void Awake()
    {
        float fullWeight = coinWeight + boostWeight + emptyWeight;
        coinWeight /= fullWeight;
        boostWeight /= fullWeight;
        emptyWeight /= fullWeight;
    }

    private void Start()
    {
        if (spawnPoints.Length == 0) return;
        Vector3 point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        float rand = Random.value;
        if (rand < coinWeight)
        {
            Instantiate(coinPrefab, transform).transform.localPosition = point;
        }
        else if (rand < coinWeight + boostWeight)
        {
            Instantiate(boostPrefab, transform).transform.localPosition = point;
        }

    }
}
