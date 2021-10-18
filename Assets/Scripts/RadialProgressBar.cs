using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RadialProgressBar : MonoBehaviour
{
    public Image radialBackground;
    public Image radialProgress;

    private float currentRadialProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        radialProgress.fillAmount = currentRadialProgress;
    }

    public void SetRadialProgress(float progress)
    {
        radialProgress.fillAmount = progress;
    }
}
