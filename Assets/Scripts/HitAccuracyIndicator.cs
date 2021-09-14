using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitAccuracyIndicator : MonoBehaviour
{
    public SpriteRenderer indicatorSprite;

    //temp this will eventually be moved and loaded from the skin
    public Sprite perfectSprite;
    public Sprite missSprite;

    [Header("Punch Scale Settings")]
    public float scaleSize = 0.3f;
    public float duration = 0.2f;
    public int vibrato = 5;
    public float elasticty = 1f;

    private void Start()
    {
        Init(HitAccuracyType.Perfect);
    }

    private void Init(HitAccuracyType accuracyType)
    {
        if (accuracyType == HitAccuracyType.Perfect)
        {
            indicatorSprite.sprite = perfectSprite;
        }
        if (accuracyType == HitAccuracyType.Perfect)
        {
            indicatorSprite.sprite = perfectSprite;
        }

        transform.DOPunchScale(new Vector3(scaleSize, scaleSize, scaleSize), duration, vibrato, elasticty);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
