using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerFollower : MonoBehaviour
{
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; 
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
