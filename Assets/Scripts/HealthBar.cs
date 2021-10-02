using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    public RectTransform health;
    //Width of parent rect to determine normalized value of health bar
    public float rectWidth = 2;
    public float tweenSpeed = 1;

    private float barPercent = 0;
    public float BarPercent { 
        get
        {
            return barPercent;
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
    }

    /// <param name="val">Normalized value of the health bar</param>
    private void SetHealthBar(float val)
    {
        float healthSize = Utilities.Remap(val, 0, 1, 2, 0);
        DOTween.To(() => barPercent, x => barPercent = x, healthSize, 1).OnUpdate(()=> {
            //Unity inverts their rect transform when setting the offset max #JustUnityThings
            health.offsetMax = new Vector2(-barPercent, 0);
        });
    }

}
