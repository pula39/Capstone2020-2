using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctionController : MonoBehaviour
{
    public void SetLocalHost()
    {
        HttpRequestTester.Instance.SetLocalhost();
    }

    public void SetRemoteHost()
    {
        HttpRequestTester.Instance.SetRemoteHost();
    }
}
