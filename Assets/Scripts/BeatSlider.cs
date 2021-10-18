using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatSlider : MonoBehaviour
{
    public Slider beatSlider;
    public Slider hitSlider;
    public RectTransform hitSliderKnob;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSlidersValue(float sliderMin, float sliderMax, float hitValue, float sliderValue)
    {
        beatSlider.minValue = sliderMin;
        beatSlider.maxValue = sliderMax;

        hitSlider.minValue = sliderMin;
        hitSlider.maxValue = sliderMax;

        hitSlider.value = hitValue;
        beatSlider.value = sliderValue;
    }

}
