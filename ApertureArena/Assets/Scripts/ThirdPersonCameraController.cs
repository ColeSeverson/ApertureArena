using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public GameObject character;
    public float mouseSens = 1.0f;
   // public float zoom;

    private Vector3 offset;
    private Vector3 lastMousePos = new Vector3(255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - character.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Magic to make the camera work
        Vector3 mouseChange = lastMousePos - Input.mousePosition;
        lastMousePos = Input.mousePosition;

        offset = Quaternion.AngleAxis(-mouseChange.x * mouseSens, Vector3.up) * offset;
        transform.position = character.transform.position + offset;
        transform.LookAt(character.transform);
    }
}
