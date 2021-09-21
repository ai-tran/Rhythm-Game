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

    public BeatCount OnBeat()
    {
        transform.DOScale(1.4f, 0.2f);
        return this;
    }

    public BeatCount OnComplete(Action onComplete)
    {
        onComplete.Invoke();
        return this;
    }

    public BeatCount OffBeat()
    {
        this.transform.DOScale(1, 0.2f);
        return this;
    }

    public BeatCount SetHitMarker()
    {
        image.color = Color.green;
        return this;
    }
}
