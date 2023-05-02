using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrowdAnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject[] skins;
    private P3dPaintableTexture[] _textures;
    private bool _isHit;
    [SerializeField] private CrowdSkinDistribution skinDistribution;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EventManager.AddListener<FinisherReplayEvent>(OnReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherReplayEvent>(OnReplay);

    }

    private void OnReplay(FinisherReplayEvent obj)
    {
        _animator.SetTrigger("replay");
        _isHit = false;
    }

    private void Start()
    {
        GameObject go = skins[skinDistribution.GetNumber(skins.Length)];
        go.SetActive(true);
        _textures = go.GetComponents<P3dPaintableTexture>();
        foreach (var texture in _textures)
        {
            texture.OnModified += TextureOnOnModified;
        }

        int rand = Random.Range(0, 3);
        _animator.SetInteger("rand", rand);
    }

    private void TextureOnOnModified(bool obj)
    {
        if (_isHit) return;
        if (Random.value < 0.5f)
            _animator.SetTrigger("hit1");
        else 
            _animator.SetTrigger("hit2");
        _isHit = true;
    }
}