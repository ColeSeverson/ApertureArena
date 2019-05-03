﻿/*
 * Written by Cole Severson
 * Handles inputs and passes them to the ThirdPersonCharacter.
 * This will be changed for the alpha, when we implement the command pattern
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    //This pattern is made to implement the command pattern for easier input management
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(ThirdPersonCameraController))]
    [RequireComponent(typeof(Image))]
    public class InputController : MonoBehaviour
    {
        public Camera mainCamera;
        public Image crossHair;
        public GameObject Bullet;
        
        private ThirdPersonCameraController cameraControl;
        private Transform cameraTransform;
        private ThirdPersonCharacter character;
        private bool jump;
        private bool crouch;
        private bool aiming;
        private bool shoot;
        private int reload = 0;

        private void Start()
        {
            character = GetComponent<ThirdPersonCharacter>();
            cameraControl = mainCamera.GetComponent<ThirdPersonCameraController>();

            cameraTransform = mainCamera.transform;
        }

        //The only things we need to do early is check for state actions like crouching or rolling
        private void Update()
        {
            if (!jump)
            {
                jump = Input.GetKeyDown(KeyCode.Space);
            }
            if (!crouch)
            {
                crouch = Input.GetKey(KeyCode.LeftControl);
            }
            if (!aiming)
            {
                aiming = Input.GetMouseButton(1);
            }
            if (!shoot)
            {
                shoot = Input.GetMouseButton(0);
            }
        }

        private void count()
        {
            if(reload > 0)
                reload = reload - 1;
        }

        private void FixedUpdate()
        {
            float v = Input.GetKey(KeyCode.W) ? 1 : 0;
            v = Input.GetKey(KeyCode.S) ? v - 1 : v;

            float h = Input.GetKey(KeyCode.D) ? 1 : 0;
            h = Input.GetKey(KeyCode.A) ? h - 1 : h;

            //print(Input.GetKey(KeyCode.W) + ", " + v + ", " + crouch);

            Vector3 cameraAngle = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * cameraAngle + h * cameraTransform.right;

            //set up the aiming
            Vector3 distance;
            if (aiming)
            {
                distance = new Vector3(0, 0, 2.5f);
                crossHair.enabled = true;
            }
            else
            {
                crossHair.enabled = false;
                distance = new Vector3(0, 0, 5f);
            }

            if (shoot && reload == 0)
            {
                reload = 2;
                GameObject bul = Instantiate(Bullet, transform.position + (cameraTransform.forward * .5f) + new Vector3(0f, 1.4f, 0f), Quaternion.identity);
                bullet temp = (bullet) bul.GetComponent(typeof(bullet));
                temp.direction = mainCamera.transform.forward;

                InvokeRepeating("count", 0.01f, 1f);
            }
          

            cameraControl.setDistance(distance);

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                move = move * 0.5f;
            }

            //print(move.ToString());

            character.Move(move, crouch, jump);

            jump = false;
            crouch = false;
            aiming = false;
            shoot = false;
        }
    }
}
 