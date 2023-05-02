using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewController : MonoBehaviour
{
    [SerializeField] private GameObject view;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private ParticleSystem boostEffect;
    [SerializeField] private ParticleSystem normalEffect;
    [SerializeField] private ParticleSystem bigEffect;
    private ParticleSystem _currentEffect;

    private void Awake()
    {
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<StartScenePlayEvent>(OnStartScenePlay);
        EventManager.AddListener<PlayerBoostEvent>(OnPlayerBoost);
        EventManager.AddListener<PlayerWeightChangeEvent>(OnPlayerWeightChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<StartScenePlayEvent>(OnStartScenePlay);
        EventManager.RemoveListener<PlayerBoostEvent>(OnPlayerBoost);
        EventManager.RemoveListener<PlayerWeightChangeEvent>(OnPlayerWeightChange);
    }

    private void OnPlayerWeightChange(PlayerWeightChangeEvent obj)
    {
        if (obj.Progress > 0.75f)
        {
            SwitchEffect(bigEffect);
        }
        else
        {
            SwitchEffect(normalEffect);
        }
    }

    private void OnPlayerBoost(PlayerBoostEvent obj)
    {
        StartCoroutine(BoostCor(1f, _currentEffect));
    }

    private IEnumerator BoostCor(float time, ParticleSystem nextEffect)
    {
        SwitchEffect(boostEffect);
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }

        SwitchEffect(nextEffect);
    }

    private void Start()
    {
        SwitchEffect(normalEffect);
    }

    private void OnStartScenePlay(StartScenePlayEvent obj)
    {
        view.transform.localScale = Vector3.zero;
    }

    private void OnGameStart(GameStartEvent obj)
    {
        view.transform.localScale = Vector3.one;
    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {
        //view.SetActive(true);
        view.transform.localScale = Vector3.one;
        view.transform.localPosition = Vector3.zero;
        StartCoroutine(DelayedEnable());
    }

    private void SwitchEffect(ParticleSystem ps)
    {
        if (_currentEffect == ps) return;
        if (_currentEffect)
        {
            _currentEffect.Stop();
        }

        _currentEffect = ps;
        _currentEffect.Play();
    }

    private IEnumerator DelayedEnable()
    {
        yield return new WaitForFixedUpdate();
        playerRB.isKinematic = false;
    }
}