using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnvDataContainer : MonoBehaviour
{
    [Serializable]
    public class ExtraInfo
    {
        public Vector3 pos;
        public GameObject prefab;
    }

    public Sprite envLetterInputSprite;

    public Sprite envBgdSprite;
    public string envName;
    public string envDesc;

    public AudioReverbPreset reverbPreset;

    public List<ExtraInfo> extraInfos;

    public List<AudioClip> bgmList;

    public List<AudioClip> soundSfxList;
}
