using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkInitializer : MonoBehaviour
{
    [SerializeField] private LevelTemplate[] templates;
    [SerializeField] private ChunkController chunkBillet;
    [SerializeField] private float chunkHeight;
    private List<ChunkController> _emptyChunks;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float despawnDistance;
    private List<ChunkController> _spawnedChunks;
    private Queue<LevelChunk> _fill;

    private LevelTemplate _currentTemplate;
    private LevelChunk _nextChunk;
    [SerializeField] private StartController startChunk;
    [Header("Debug")] [SerializeField] private bool spawnSpecific;
    [SerializeField] private LevelTemplate specTemplate;
    private void Awake()
    {
        _emptyChunks = new List<ChunkController>();
        _spawnedChunks = new List<ChunkController>();
        int level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
        #if UNITY_EDITOR
        if (spawnSpecific)
            _currentTemplate = specTemplate;
        else
            _currentTemplate = templates[level % templates.Length];
        _fill = new Queue<LevelChunk>(_currentTemplate.Chunks);
        #else
        _currentTemplate = templates[level % templates.Length];
        _fill = new Queue<LevelChunk>(_currentTemplate.Chunks);
        #endif
    }

    private void Start()
    {
        int i;
        for (i = 0; i < _currentTemplate.Chunks.Length; i++)
        {
            ChunkController chunkTransform = Instantiate(chunkBillet, transform);
            _emptyChunks.Add(chunkTransform);
            chunkTransform.transform.localPosition = Vector3.up * chunkHeight * i;
        }
        _emptyChunks.Reverse();
        startChunk.transform.position = transform.position + Vector3.up * chunkHeight * (i + 2);
        startChunk.SetPlayer();
    }

    private void Update()
    {
        if (_emptyChunks.Count > 0 && playerTransform.position.y - _emptyChunks[0].transform.position.y < spawnDistance)
        {
            _nextChunk = _fill.Dequeue();
            _emptyChunks[0].FillChunk(_nextChunk);
            _emptyChunks[0].SpawnBonuses(_nextChunk.Bonuses);
            _spawnedChunks.Add(_emptyChunks[0]);
            _emptyChunks.RemoveAt(0);
        }

        if (_spawnedChunks.Count > 0 && _spawnedChunks[0].transform.position.y - playerTransform.position.y > despawnDistance)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    /*private void FillChunk(LevelChunk nextChunk, Transform chunkTransform)
    {
        Transform fillTransform = Instantiate(nextChunk.Fill, chunkTransform).transform;
        fillTransform.eulerAngles = Vector3.up * nextChunk.Rotation;
    }*/
}
