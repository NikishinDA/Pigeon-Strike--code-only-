using System;
using System.Collections;
using System.Collections.Generic;
using PaintIn3D;
using UnityEngine;

public class PaintableMaterialRefresh : MonoBehaviour
{
    private Material[] _originalMaterials;
    private Renderer _renderer;
    private P3dPaintableTexture[] _paintTextures;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterials = _renderer.materials;
        _paintTextures = GetComponents<P3dPaintableTexture>();
       // foreach (var texture in _paintTextures)
        //{
            //texture.StoreState();
        //}
        EventManager.AddListener<FinisherReplayEvent>(OnFinisherReplay);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<FinisherReplayEvent>(OnFinisherReplay);

    }

    private void OnFinisherReplay(FinisherReplayEvent obj)
    {
        foreach (var texture in _paintTextures)
        {
            texture.Clear();
        }
    }
}
