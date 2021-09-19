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

    public void Init(HitAccuracy accuracyType)
    {
        if (accuracyType == HitAccuracy.Perfect)
        {
            indicatorSprite.sprite = GameManager.Instance.currentSkin.perfectText;
        }
        if (accuracyType == HitAccuracy.Miss)
        {
            indicatorSprite.sprite = GameManager.Instance.currentSkin.missText;
        }
        transform.DOPunchScale(new Vector3(scaleSize, scaleSize, scaleSize), duration, vibrato, elasticty);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
