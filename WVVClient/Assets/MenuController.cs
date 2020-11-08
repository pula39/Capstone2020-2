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

    public GameObject menuRoot;

    public GameObject menuBackGround;

    public GameObject menuBar;

    public void Awake()
    {
        menuObjects = new List<GameObject>();
        foreach (Transform t in menuRoot.transform)
        {
            if (t.gameObject == menuBackGround)
            {
                continue;
            }

            menuObjects.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        menuBackGround.SetActive(false);
    }

    public void ActiveMenu(GameObject menuGb)
    {
        foreach (GameObject gb in menuObjects)
        {
            gb.SetActive(gb == menuGb);
        }

        menuBackGround.SetActive(true);
    }

    public void ToggleUI()
    {
        var currentlyActive = menuBar.gameObject.activeSelf;
        foreach (GameObject gb in menuObjects)
        {
            gb.SetActive(false);
        }

        menuBackGround.SetActive(false);

        menuBar.SetActive(!currentlyActive);
    }
}
