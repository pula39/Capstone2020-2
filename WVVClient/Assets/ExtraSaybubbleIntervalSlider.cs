using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtraSaybubbleIntervalSlider : MonoBehaviour
{
    public Slider mainSlider;
    public ExtraCharacter extra;
    public TMP_Text valueText;

    public void Awake()
    {
        mainSlider = GetComponent<Slider>();

        mainSlider.minValue = 10;
        mainSlider.maxValue = 60;

        mainSlider.value = extra.randomSayInteval;

        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        valueText.text = $"{mainSlider.value:0.##}초";
    }

    public void ValueChangeCheck()
    {
        extra.randomSayInteval = mainSlider.value;

        valueText.text = $"{mainSlider.value:0.##}초";
    }
}
