using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnvMenuDisplay : MonoBehaviour
{
    public Button envButton;

    public Image envImage;
    public TMP_Text envName;
    public TMP_Text envDesc;
    public TMP_Text envReverbPreset;

    public void ShowEnvData(EnvDataContainer data)
    {
        envImage.sprite = data.envBgdSprite;

        envName.text = data.envName;
        envDesc.text = data.envDesc;
        envReverbPreset.text = data.reverbPreset.ToString();
    }

    public void AddButtonClickEvent(UnityAction action)
    {
        envButton.onClick.AddListener(action);
    }
}
