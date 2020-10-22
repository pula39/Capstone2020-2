using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageSendController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField msgInput;
    public SayBubble charSayBubble;

    public void Awake()
    {
        charSayBubble.HideBubble();
    }

    public void SendLetter()
    {
        if (nameInput.text.Length == 0)
        {
            nameInput.SetTextWithoutNotify("ㅇㅇ");
        }

        if (msgInput.text.Length == 0)
        {
            Debug.LogError("메시지라도 입력해야지");
            return;
        }

        var username = nameInput.text;
        var msg = msgInput.text;

        HttpRequestTester.Instance.StartCoroutine(HttpRequestTester.Instance.PostLetter(username, msg, (ret_id) =>
        {
            if (ret_id < 0)
            {
                charSayBubble.SetSayText("에러가 발생하였습니다");
                charSayBubble.StartDisplayBubble();
                return;
            }

            nameInput.text = "";
            msgInput.text = "";

            charSayBubble.SetSayText(msg);
            GetVoice(ret_id);
        }));
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
