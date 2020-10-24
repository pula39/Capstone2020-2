using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HttpRequestTester : MonoBehaviour
{
    public static int timeout = 10;
    private static HttpRequestTester instance;
    public static HttpRequestTester Instance
    {
        get
        {
            if (instance == null)
            {
                HttpRequestTester gb = FindObjectOfType<HttpRequestTester>();
                instance = gb;
            }

            return instance;
        }
    }

    private string _baseUrl = "https://wvv-server.herokuapp.com/";
    private string baseUrl
    {
        get
        {
            if (PlayerPrefs.GetString("Custom Server", "") == "")
            {
                return _baseUrl;
            }

            return PlayerPrefs.GetString("Custom Server");
        }
    }
    //private string baseUrl = "http://localhost:5000/";
#if UNITY_EDITOR
    [UnityEditor.MenuItem("DebugMenu/TestHttpRequest")]
#endif

    public IEnumerator Upload(string url, WWWForm form, Action<string> action)
    {
        var finalUrl = baseUrl + url;
        if (form == null)
        {
            form = new WWWForm();
        }

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            www.timeout = timeout;

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                action.Invoke(null);
            }
            else
            {
                action.Invoke(www.downloadHandler.text);
            }
        }
    }


    public IEnumerator Get(string url, Action<string> action)
    {
        var finalUrl = baseUrl + url;
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get(finalUrl))
        {
            www.timeout = timeout;

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                action.Invoke(null);
            }
            else
            {
                action.Invoke(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetLetterVoice(int id, UnityAction<AudioClip> action)
    {
        string url = baseUrl + $"letter/voice/{id}";
        using (UnityWebRequest www =  UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
        {
            www.timeout = timeout;

            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                action.Invoke(null);
            }
            else
            {
                action(DownloadHandlerAudioClip.GetContent(www));
            }
        }
    }

    public IEnumerator PostLetter(string username, string dialog, string voiceType, Action<int> action)
    {
        string url = baseUrl + $"letter/add";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("dialog", dialog);
        form.AddField("voice_type", voiceType);

        yield return Upload(url, form, (ret) =>
        {
            if (String.IsNullOrEmpty(ret))
            {
                action.Invoke(-1);
            }
            else
            {
                action.Invoke(Int32.Parse(ret));
            }
        });
    }
}
