using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SayBubble : MonoBehaviour
{
    public GameObject sayBubbleLayout;

    public TMP_Text sayText;
    public TMP_Text userName;

    public AudioSource audioSource; 
    public float hideTime = 5.0f;

    public UnityEvent onDisplayBubble = new UnityEvent(); 
    public UnityEvent onHideBubble = new UnityEvent();

    public void HideBubble()
    {
        sayBubbleLayout.gameObject.SetActive(false);
    }

    public void ShowBubble()
    {
        sayBubbleLayout.gameObject.SetActive(true);
    }

    public void SetSayText(string serihu)
    {
        sayText.text = serihu;
    }

    public void SetUserName(string name)
    {
        // null일 수 있음
        if (userName)
        {
            userName.text = name;
        }
    }

    public void SetSound(AudioClip audio)
    {
        audioSource.clip = audio;
    }


    public void StartDisplayBubble()
    {
        StartCoroutine(DisplayBubble());
    }

    private IEnumerator DisplayBubble()
    {
        ShowBubble();

        var finalHideTime = hideTime;
        if (audioSource.clip != null)
        {
            hideTime = Mathf.Max(hideTime, audioSource.clip.length + 1.0f);
            audioSource.Play();
        }

        onDisplayBubble.Invoke();
        yield return new WaitForSeconds(hideTime);
        onHideBubble.Invoke();

        audioSource.clip = null;

        HideBubble();;
    }
}
