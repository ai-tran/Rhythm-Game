using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public RectTransform health;

    private float rectWidth;

    private float healthBarVal = 0;
    public float HealthBarVal { 
        get
        {
            return healthBarVal;
        }
        set
        {
            SetHealthBar(value);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        rectWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        SetHealthBar(0.5f);
    }

    private void SetHealthBar(float val)
    {
        DOTween.To(() => healthBarVal, x => healthBarVal = x, 2, 1);
        health.sizeDelta = new Vector2(healthBarVal, health.sizeDelta.y);
    }

}
