using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LetterInfo 
{
    public string username;
    public string dialog;
    public int id;
}
public class TestTextSender : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text text;

    public AudioSource charaAudio;
}
