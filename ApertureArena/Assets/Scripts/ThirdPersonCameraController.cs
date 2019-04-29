using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonCameraController : MonoBehaviour
{
    public GameObject character;
    public float mouseSens = 1.0f;
    public Text debugText;
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

        Vector3 offset1 = Quaternion.AngleAxis(-mouseChange.x * mouseSens, Vector3.up) * offset;
        Vector3 offset2 = new Vector3(0,0,0);
        if (!((mouseChange.y < 0 && transform.position.y > 4) || (mouseChange.y > 0 && transform.position.y < -2)))
            offset2 = Quaternion.AngleAxis(-mouseChange.y * mouseSens, Vector3.left) * offset;

        transform.position = character.transform.position + (offset1 + offset2);
        /*if (transform.position.y > 4)
            transform.position = new Vector3(transform.position.x, 4, transform.position.z);
        
        if (transform.position.y < -2)
            transform.position = new Vector3(transform.position.x, -2, transform.position.z);
        */
        transform.LookAt(character.transform);

        if (debugText)
        {
            debugText.text = transform.position.ToString();
        }
    }
}
