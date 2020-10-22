using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    public AudioReverbZone audioReverbZone;

    public Button audioReverbSampleButton;


    public void Awake()
    {
        MakeReverbAdjustButton();
    }

    public void MakeReverbAdjustButton()
    {
        var t = audioReverbSampleButton.transform.parent;
        var enumList = Enum.GetNames(typeof(AudioReverbPreset));
        foreach (var audioReverbPreset in enumList)
        {
            var enumed = (AudioReverbPreset)Enum.Parse(typeof(AudioReverbPreset), audioReverbPreset);
            var gb = GameObject.Instantiate(audioReverbSampleButton.gameObject, t);
            gb.GetComponent<Button>().onClick.AddListener(() => { audioReverbZone.reverbPreset = enumed;});
            gb.GetComponentInChildren<Text>().text = audioReverbPreset;
        }
    }
}
