using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinisherController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 _playerEnterPosition;
    [SerializeField] private GameObject[] cameras;
    private GameObject _activeCamera;
    private int _cameraOrd;
    [SerializeField] private Transform _crowdTransform;
    private float[] rotMas = {-90, 0, 90, 180};
    private void Awake()
    {
        EventManager.AddListener<FinisherPlayerReachGroundEvent>(OnPlayerReachGround);
        EventManager.AddListener<FinisherStartEvent>(OnFinisherStart);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherPlayerReachGroundEvent>(OnPlayerReachGround);
        EventManager.RemoveListener<FinisherStartEvent>(OnFinisherStart);

    }

    private void Start()
    {
        _crowdTransform.Rotate(Vector3.up, rotMas[Random.Range(0, rotMas.Length)]);
    }

    private void OnFinisherStart(FinisherStartEvent obj)
    {
        _playerEnterPosition = obj.PlayerPosition;
    }

    private void OnPlayerReachGround(FinisherPlayerReachGroundEvent obj)
    {
        StartCoroutine(ReplayCor());
    }

    private IEnumerator ReplayCor()
    {
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        EventManager.Broadcast(GameEventsHandler.FinisherReplayEvent);
        playerTransform.position = _playerEnterPosition;
        if (_activeCamera)
            _activeCamera.SetActive(false);
        _activeCamera = cameras[_cameraOrd++ % cameras.Length];
        _activeCamera.SetActive(true);
        Time.timeScale = 0.5f;
    }
}
