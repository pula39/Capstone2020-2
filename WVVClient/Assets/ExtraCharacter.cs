using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCharacter : MonoBehaviour
{
    public SayBubble sayBubble;

    public float randomSayInteval = 60.0f;

    public AudioClip GeneralAudioClip;

    public AudioSource SfxAudioSource;

    void Awake()
    {
        sayBubble.HideBubble();
    }

    void Start()
    {
        StartCoroutine(DisplayBubble());
    }

    public void ChangeRandomSayInterval(float interval)
    {
        StopAllCoroutines();
        
        sayBubble.HideBubble();
        randomSayInteval = interval;

        StartCoroutine(DisplayBubble());
    } 

    public IEnumerator DisplayBubble()
    {
        
        yield return new WaitForSeconds(randomSayInteval * Random.Range(0.8f, 1.2f));

        GetRandomLetter();

        StartCoroutine(DisplayBubble());
    }

    public void GetRandomLetter()
    {
        HttpRequestTester.Instance.StartCoroutine(HttpRequestTester.Instance.Get("letter/random", (ret_string) =>
        {
            if (ret_string != null)
            {
                var letterInfo = JsonUtility.FromJson<LetterInfo>(ret_string);
                sayBubble.SetSayText(letterInfo.dialog);
                sayBubble.SetUserName(letterInfo.username);

                GetVoice(letterInfo.id);
            }
            else
            {
                sayBubble.SetUserName("[   ]");
                sayBubble.SetSayText("....");

                sayBubble.StartDisplayBubble();
            }
        }));
    }

    public void GetVoice(int id)
    {
        HttpRequestTester.Instance.StartCoroutine(HttpRequestTester.Instance.GetLetterVoice(id, (letterClip) =>
        {
            if (letterClip != null)
            {
                sayBubble.SetSound(letterClip);
            }
            else
            {
                sayBubble.SetSound(GeneralAudioClip);
            }

            sayBubble.StartDisplayBubble();
        }));
    }
}
