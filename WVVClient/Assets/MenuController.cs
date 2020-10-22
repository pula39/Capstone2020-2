using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> menuObjects;

    public GameObject MenuRoot;

    public void Awake()
    {
        menuObjects = new List<GameObject>();
        foreach (Transform t in MenuRoot.transform)
        {
            menuObjects.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
    }

    public void ActiveMenu(GameObject menuGb)
    {
        foreach (GameObject gb in menuObjects)
        {
            gb.SetActive(gb == menuGb);
        }
    }
}
