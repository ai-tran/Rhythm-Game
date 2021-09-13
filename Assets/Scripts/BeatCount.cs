using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BeatCount : MonoBehaviour
{
    public SpriteRenderer sprite;

    public void OnBeat()
    {
        this.transform.DOScale(1.2f, 0.2f);
    }

    public void OffBeat()
    {
        this.transform.DOScale(1, 0.2f);
    }

    public void SetHitMarker()
    {
        sprite.color = Color.green;
    }
}
