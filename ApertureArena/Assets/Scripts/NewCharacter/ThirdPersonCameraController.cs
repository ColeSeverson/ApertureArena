/*
 * Written By Cole Severson
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterController
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        public GameObject character;
        public float mouseSens = 3.0f;
        public Text debugText;
        public float YAngleMax = 25f;
        public float YAngleMin = -45f;

        private Vector3 distance = new Vector3(0, 0, 5f);


        private Vector3 lastMousePos = new Vector3(255, 255, 255);
        //private Vector3 mouseChange = new Vector3(0,0,0);
        private Vector2 cameraAngle = new Vector2(0, -25);
        // Start is called before the first frame update
        void Start()
        {

        }
        public void setDistance(Vector3 v)
        {
            distance = v;
        }
        public Vector3 getDistance()
        {
            return distance;
        }
        // Update is called once per frame
        void Update()
        {
            //In update we will get the mouse inputs so that they can be processed in LateUpdate
            //lastMousePos = Input.mousePosition;
            cameraAngle.x += Input.GetAxis("Mouse X") * mouseSens;
            cameraAngle.y += Input.GetAxis("Mouse Y") * mouseSens;

            cameraAngle.y = Mathf.Clamp(cameraAngle.y, YAngleMin, YAngleMax);
            //debugText.text = cameraAngle.ToString();
        }
        private void LateUpdate()
        {
            //In LateUpdate we will actually process the mouse inputs
            //Direction is the base position behind the player the camera should be
            Vector3 direction = -distance;
            //rotation is the calulated rotation position
            Quaternion rotation = Quaternion.Euler(-cameraAngle.y, cameraAngle.x, 0);
            //mulitplying rotation and direction gives the calulated position that the camera will end up in
            Vector3 head = character.transform.position + new Vector3(0, 1.5f, 0);

            transform.position = head + rotation * direction;
            transform.LookAt(head);
        }
    }
}
