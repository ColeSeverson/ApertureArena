using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    //This pattern is made to implement the command pattern for easier input management
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class InputController : MonoBehaviour
    {
        public Camera mainCamera;

        private Transform cameraTransform;
        private ThirdPersonCharacter character;
        private bool jump;
        private bool crouch;

        private void Start()
        {
            character = GetComponent<ThirdPersonCharacter>();
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

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                move = move * 0.5f;
            }

            //print(move.ToString());

            character.Move(move, crouch, jump);

            jump = false;
            crouch = false;
        }
    }
}
 