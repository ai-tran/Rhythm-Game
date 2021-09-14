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

    public float punchScaleSize = 0.3f;

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

        transform.DOPunchScale(new Vector3(0.2f,0.2f,0.2f), 0.2f, 5, 1);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
