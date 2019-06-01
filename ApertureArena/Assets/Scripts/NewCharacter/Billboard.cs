using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simple script that keeps an object looking at the camera, for text;
public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
