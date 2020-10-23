using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertManager : SingletonObject<AlertManager>
{
    public GameObject alertParent;
    public TMP_Text alertMsgText;
    // Start is called before the first frame update

    protected override void PostAwake()
    {
        alertParent.SetActive(false);
    }

    public void ShowAlertMsg(string str)
    {
        StartCoroutine(ShowAlertForSeconds(str, 5.0f));
    }

    public IEnumerator ShowAlertForSeconds(string msg, float seconds)
    {
        alertMsgText.text = msg;
        alertParent.SetActive(true);

        yield return new WaitForSeconds(seconds);

        alertParent.SetActive(false);
    }
}
