using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvController : MonoBehaviour
{
    [Serializable]
    public class EnvDataToMenu
    {
        public EnvDataContainer data;
        public EnvMenuDisplay display;
    }

    public SpriteRenderer background; 
    public SpriteRenderer letterSpriteRenderer;
    private Sprite defaultLetterSpriteRenderer;

    public AudioReverbZone reverbZone;

    public GameObject extraRoot;

    public List<EnvDataToMenu> envDataToMenu;

    public SoundSlider[] soundSliderForExtra;
    public List<GameObject> extraList;

    void Awake()
    {
        var jsonStr = "{}";

        SetEnvData(envDataToMenu[0].data);

        SetEnvDataDisplay();

        defaultLetterSpriteRenderer = letterSpriteRenderer?.sprite;
    }

    public void SetEnvData(EnvDataContainer data)
    {
        background.sprite = data.envBgdSprite;
        reverbZone.reverbPreset = data.reverbPreset;

        foreach (Transform t in extraRoot.transform)
        {
            Destroy(t.gameObject);
        }

        extraList.Clear();
        foreach (var dataExtraInfo in data.extraInfos)
        {
            var gb = Instantiate(dataExtraInfo.prefab, extraRoot.transform);
            gb.transform.position = dataExtraInfo.pos;
            extraList.Add(gb);

        }

        //하드코딩
        for (int i = 0; i < soundSliderForExtra.Length; i++)
        {
            if (i >= extraList.Count)
            {
                break;
            }

            soundSliderForExtra[i].audioSource = extraList[i].GetComponentInChildren<AudioSource>();
        }

        if (data.envLetterInputSprite != null)
        {
            letterSpriteRenderer.sprite = data.envLetterInputSprite;
        }
    }

    public void SetEnvDataDisplay()
    {
        foreach (var dataToMenu in envDataToMenu)
        {
            dataToMenu.display.ShowEnvData(dataToMenu.data);
            dataToMenu.display.AddButtonClickEvent(() =>
            {
                SetEnvData(dataToMenu.data);
            });
        }

    }
}
