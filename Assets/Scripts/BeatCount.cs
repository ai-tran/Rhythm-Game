using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BeatCount : MonoBehaviour
{
    public Image image;

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
        image.color = Color.green;
    }
}
