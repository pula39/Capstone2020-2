using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public ExtraSaybubbleIntervalSlider[] extraSaybubbleIntervalSlider;


    public List<ExtraCharacter> extraList;

    public EnvDataContainer currentEnvDataContainer;

    public AudioSource BgmAudioSource;

    public GameObject characterModel;

    public float randomSFXPlayMin = 15;
    public float randomSFXPlayMax = 45;

    void Awake()
    {
        var jsonStr = "{}";

        SetEnvData(envDataToMenu[0].data);

        SetEnvDataDisplay();

        defaultLetterSpriteRenderer = letterSpriteRenderer?.sprite;
    }

    public void SetEnvData(EnvDataContainer data)
    {
        currentEnvDataContainer = data;

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
            extraList.Add(gb.GetComponent<ExtraCharacter>());

        }

        // 사운드 지정을 위한 하드코딩
        for (int i = 0; i < soundSliderForExtra.Length; i++)
        {
            if (i >= extraList.Count)
            {
                break;
            }

            soundSliderForExtra[i].audioSource = extraList[i].GetComponentInChildren<AudioSource>();
            extraSaybubbleIntervalSlider[i].extra = extraList[i];
            if (extraSaybubbleIntervalSlider[i].mainSlider)
            {
                extraList[i].randomSayInteval = extraSaybubbleIntervalSlider[i].mainSlider.value;
            }
        }

        if (data.envLetterInputSprite != null)
        {
            letterSpriteRenderer.sprite = data.envLetterInputSprite;
        }

        StopAllCoroutines();
        StartCoroutine(PlayEnvBgmLoop());
        StartCoroutine(PlayEnvSFXLoop());
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

    public IEnumerator PlayEnvBgmLoop()
    {
        float interval = 10.0f;

        foreach (var audioClip in currentEnvDataContainer.bgmList)
        {
            BgmAudioSource.clip = audioClip;
            BgmAudioSource.loop = false;
            BgmAudioSource.Play();

            yield return new WaitForSeconds(audioClip.length + interval);
        }

        yield return new WaitForSeconds(interval);

        StartCoroutine(PlayEnvBgmLoop());
    }

    public IEnumerator PlayEnvSFXLoop()
    {
        yield return new WaitForSeconds(Random.Range(randomSFXPlayMin, randomSFXPlayMax));

        while (true)
        {
            var selectedClipIndex = Random.Range(0, currentEnvDataContainer.soundSfxList.Count);
            var selectedClip = currentEnvDataContainer.soundSfxList[selectedClipIndex];

            var interval = Math.Max(Random.Range(randomSFXPlayMin, randomSFXPlayMax), selectedClip.length);

            if (extraList.Count != 0)
            {
                var extra = extraList[Random.Range(0, extraList.Count)];
                if (selectedClip == null)
                {
                    AlertManager.Instance.ShowAlertForSeconds($"EXTRA SFX 가 null입니다. {selectedClipIndex}", 0.5f);
                }
                else
                {
                    extra.SfxAudioSource.PlayOneShot(selectedClip);
                    AlertManager.Instance.ShowAlertMsg($"EXTRA가 SFX를 재생하였습니다.{selectedClip.name}");
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }

    public void SetCharacterVisible(bool visible)
    {
        characterModel.SetActive(visible);
    }
}
