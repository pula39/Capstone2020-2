using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageSendController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField msgInput;
    public SayBubble charSayBubble;
    public CharacterController characterController;
    public string voiceType = "mijin";

    [Serializable]
    public class ToggleToVoiceType
    {
        public Toggle toggle;
        public string voiceType;
    }

    public List<ToggleToVoiceType> toggleToVoiceTypeList;

    public void Awake()
    {
        charSayBubble.HideBubble();

        nameInput.text = PlayerPrefs.GetString("Player Name", "");

        charSayBubble.onDisplayBubble.AddListener(() => { characterController.eyeOpening = 0.0f; });
        charSayBubble.onHideBubble.AddListener(() => { characterController.eyeOpening = 1.0f; });
    }

    public void SetVoiceType()
    {
        foreach (var toggleToVoiceType in toggleToVoiceTypeList)
        {
            if (toggleToVoiceType.toggle.isOn)
            {
                voiceType = toggleToVoiceType.voiceType;
            }
        }
    }

    public void SendLetter()
    {
        SetVoiceType();

        if (nameInput.text.Length == 0)
        {
            AlertManager.Instance.ShowAlertMsg("이름을 입력해주세요.");
            return;
        }

        if (msgInput.text.Length == 0)
        {
            AlertManager.Instance.ShowAlertMsg("메시지를 입력해주세요.");
            return;
        }

        var username = nameInput.text;
        var msg = msgInput.text;

        HttpRequestTester.Instance.StartCoroutine(HttpRequestTester.Instance.PostLetter(username, msg, voiceType,
        (ret_id) =>
        {
            if (ret_id < 0)
            {
                AlertManager.Instance.ShowAlertMsg("메시지 송신 시 문제가 발생했습니다.");
                charSayBubble.StartDisplayBubble();
                return;
            }

            msgInput.text = "";

            charSayBubble.SetSayText(msg);
            GetVoice(ret_id);
        }));

        PlayerPrefs.SetString("Player Name", username);
    }

    public void GetVoice(int id)
    {
        HttpRequestTester.Instance.StartCoroutine(HttpRequestTester.Instance.GetLetterVoice(id, (letterClip) =>
        {
            if (letterClip != null)
            {
                charSayBubble.SetSound(letterClip);
            }
            else
            {
                charSayBubble.SetSound(null);
            }

            charSayBubble.StartDisplayBubble();
        }));
    }
}
