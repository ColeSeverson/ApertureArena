using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
  Cole Severson
  This class was written by Cole Severson with the help of several stack overflow articles about camera control in Unity
*/

public class CameraController : MonoBehaviour
{
    public GameObject character;
    //public float mouseSens = 1.0f;
    public float mouseSens = 3.0f;
    public Text debugText;
   // public float zoom;
    public float YAngleMax = 25f;
    public float YAngleMin = -45f;
    // public float zoom;

  //  private Vector3 offset;

    private float distance = 1.0f;
    private Vector3 lastMousePos = new Vector3(255, 255, 255);
    //private Vector3 mouseChange = new Vector3(0,0,0);
    private Vector2 cameraAngle = new Vector2(0, -25);
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - character.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Magic to make the camera work
      /*  Vector3 mouseChange = lastMousePos - Input.mousePosition;
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

        transform.LookAt(character.transform); */
        //In update we will get the mouse inputs so that they can be processed in LateUpdate
        //lastMousePos = Input.mousePosition;
        cameraAngle.x += Input.GetAxis("Mouse X") * mouseSens;
        cameraAngle.y += Input.GetAxis("Mouse Y") * mouseSens;

      /*  if (debugText)
        {
            debugText.text = transform.position.ToString();
        }*/
        cameraAngle.y = Mathf.Clamp(cameraAngle.y, YAngleMin, YAngleMax);
        debugText.text = cameraAngle.ToString();
    }
    private void LateUpdate()
    {
        //In LateUpdate we will actually process the mouse inputs
        //pos is the base position behind the player the camera should be
        Vector3 pos = new Vector3(0, 0, -4f);
        //rotation is the calulated rotation position
        Quaternion rotation = Quaternion.Euler(-cameraAngle.y, cameraAngle.x, 0);
        //mulitplying rotation and direction gives the calulated position that the camera will end up in
        transform.position = character.transform.position + new Vector3(0, 2f, 0)  + rotation * pos;
        transform.LookAt(character.transform.position + new Vector3(0, 2f, 0));
    }
}
