﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController {

  [RequireComponent(typeof(ThirdPersonCharacter))]
  //[RequireComponent(typeof(ThirdPersonCameraController))]
  public class InputController : MonoBehaviour
  {
    //Public Vars
    public Camera mainCamera;
    public float speed = 10f;

    //Private Vars
    //private ThirdPersonCameraController cameraController;
    private ThirdPersonCharacter character;
    private Transform cameraTransform;
    private Command moveAxis, buttonSpace, buttonControl, buttonShift, buttonLeftMouse, buttonRightMouse;
    private float lastHorz, lastVert;


    private void Start() {
      character = GetComponent<ThirdPersonCharacter>();
      cameraTransform = mainCamera.transform;

      moveAxis = new Move();
      buttonSpace = new Jump();
      buttonControl = new Blink();
      buttonShift = new Sprint();
      buttonLeftMouse = new Attack();
      buttonRightMouse = new ADS();
    }

    private void Update() {
      handleInput();
    }

    private void handleInput() {
      float horz = Input.GetAxisRaw("Horizontal");
      float vert = Input.GetAxisRaw("Vertical");

      //These checks queue up actions such as jump or sprint, then call a move.
      if (Input.GetKey(KeyCode.LeftControl)) {
        buttonControl.Execute(character);
      }
      if(Input.GetMouseButtonDown(0))
        buttonLeftMouse.Execute();

      buttonSpace.Execute(Input.GetKey(KeyCode.Space));
      buttonShift.Execute(Input.GetKey(KeyCode.LeftShift));
      buttonRightMouse.Execute(Input.GetMouseButtonDown(1));
      moveAxis.Execute(character, cameraTransform, new Vector3(horz, 0, vert));

      lastHorz = horz;
      lastVert = vert;
    }

  }
}
