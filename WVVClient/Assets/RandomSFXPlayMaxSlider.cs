using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSFXPlayMaxSlider : MonoBehaviour
{
    public Slider mainSlider;
    public EnvController envController;
    public RandomSFXPlayMinSlider randomSfxPlayMin;
    public TMP_Text valueText;

    public void Awake()
    {
        mainSlider = GetComponent<Slider>();

        mainSlider.minValue = 10;
        mainSlider.maxValue = 60;

        mainSlider.value = envController.randomSFXPlayMax;

        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        valueText.text = $"{mainSlider.value:0.##}초";
    }

    public void ValueChangeCheck()
    {
        if (randomSfxPlayMin.mainSlider.value > mainSlider.value)
        {
            randomSfxPlayMin.mainSlider.value = mainSlider.value;
        }

        envController.randomSFXPlayMax = mainSlider.value;

        valueText.text = $"{mainSlider.value:0.##}초";
    }
}
