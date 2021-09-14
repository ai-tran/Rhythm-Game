using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to destroy this object after animation is done
public class HitIndicatorEvents : MonoBehaviour
{
    private HitAccuracyIndicator hitAccuracyIndicator;

    // Start is called before the first frame update
    void Start()
    {
        hitAccuracyIndicator = transform.parent.GetComponent<HitAccuracyIndicator>();
    }

    //triggered in animation event
    public void OnAnimationComplete()
    {
        hitAccuracyIndicator.Destroy();
    }
}
