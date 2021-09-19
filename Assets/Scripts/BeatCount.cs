using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class BeatCount : MonoBehaviour
{
    public Image image { get; set; }

    private void Awake()
    {
        image = transform.GetComponent<Image>();
        image.sprite = GameManager.Instance.currentSkin.beatCount;
    }

    public void OnBeat()
    {
        transform.DOScale(1.2f, 0.2f);
    }

    public BeatCount OnComplete(Action onComplete)
    {
        onComplete.Invoke();
        return this;
    }

    public void OffBeat()
    {
        this.transform.DOScale(1, 0.2f);
    }

    public void SetHitMarker()
    {
        image.color = Color.green;
    }
}
