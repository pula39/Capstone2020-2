using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSFXPlayMinSlider : MonoBehaviour
{
    public Slider mainSlider;
    public EnvController envController;
    public RandomSFXPlayMaxSlider randomSfxPlayMax;
    public TMP_Text valueText;


    public void Awake()
    {
        mainSlider = GetComponent<Slider>();

        mainSlider.minValue = 10;
        mainSlider.maxValue = 60;

        mainSlider.value = envController.randomSFXPlayMin;

        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        valueText.text = $"{mainSlider.value:0.##}초";
    }

    public void ValueChangeCheck()
    {
        if (randomSfxPlayMax.mainSlider.value < mainSlider.value)
        {
            randomSfxPlayMax.mainSlider.value = mainSlider.value;
        }

        envController.randomSFXPlayMin = mainSlider.value;

        valueText.text = $"{mainSlider.value:0.##}초";
    }
}
