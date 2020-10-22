using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomEnv 
{
    public string envName;
    public AudioReverbPreset reverbZonePreset;
    public string bgdFileName;
}
public class EnvController : MonoBehaviour
{
    public CustomEnv customEnv;

    public Sprite background; 
    public AudioReverbZone reverbZone; 

    void Awake()
    {
        var jsonStr = "{}";
        JsonUtility.FromJsonOverwrite(jsonStr, customEnv);

        reverbZone.reverbPreset = customEnv.reverbZonePreset;
        // 사진 추가는 나중에
    }
}
