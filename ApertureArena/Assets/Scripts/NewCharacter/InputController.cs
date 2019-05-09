using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController {

  [RequireComponent(typeof(ThirdPersonCharacter))]
  [RequireComponent(typeof(ThirdPersonCameraController))]
  public class InputController : MonoBehaviour
  {
    //Public Vars
    public Camera mainCamera;
    public float speed = 10f;

    //Private Vars
    //private ThirdPersonCameraController cameraController;
    private ThirdPersonCharacter character;
    private Transform cameraTransform;
    private Command moveAxis, buttonSpace, buttonControl, buttonShift;

    private void Start() {
      character = GetComponent<ThirdPersonCharacter>();
      cameraTransform = mainCamera.transform;

      moveAxis = new Move();
      buttonSpace = new Jump();
      buttonControl = new Crouch();
      buttonShift = new Sprint();
    }

    private void Update() {
      handleInput();
    }

    private void handleInput() {
      float horz = Input.GetAxis("Horizontal");
      float vert = Input.GetAxis("Vertical");

      //check for inputs
      if (horz != 0 || vert != 0) {
        //Vector3 direction = new Vector3(horz, 0, vert);
        moveAxis.Execute(character, cameraTransform, new Vector3(horz, 0, vert));
      }
      buttonSpace.Execute(Input.GetKey(KeyCode.Space));
      buttonControl.Execute(Input.GetKey(KeyCode.LeftControl));
      buttonShift.Execute(Input.GetKey(KeyCode.LeftShift));
    }

  }
}
