using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private Slider mainSlider;
    public AudioSource audioSource;

    public void Awake()
    {
        mainSlider = GetComponent<Slider>();

        mainSlider.value = audioSource.volume;
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        audioSource.volume = mainSlider.value;
    }
}
