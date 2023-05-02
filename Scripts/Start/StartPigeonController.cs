using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartPigeonController : MonoBehaviour
{
    private Vector3 _startScale;
    private void Awake()
    {
        EventManager.AddListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);
        _startScale = transform.localScale;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UpgradeButtonPressEvent>(OnUpgradeButtonPress);
    }

    private void OnUpgradeButtonPress(UpgradeButtonPressEvent obj)
    {
        ChangeSize();
    }
    private void ChangeSize()
    {
        Sequence sizeBumpSequence = DOTween.Sequence();
        sizeBumpSequence.Append(
            transform.DOScale(Vector3.Scale(_startScale, new Vector3( 0.8f,1.5f, 0.8f)) , 0.1f));
        sizeBumpSequence.Append(
            transform.DOScale(Vector3.Scale(_startScale, new Vector3(1.75f, 0.8f, 1.75f)) , 0.25f));
        sizeBumpSequence.Append(transform.DOScale(_startScale, 0.25f));
        sizeBumpSequence.Play();
    }
}
