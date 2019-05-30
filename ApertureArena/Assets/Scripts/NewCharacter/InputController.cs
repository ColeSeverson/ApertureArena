using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Cole Severson
Written to follow the command pattern
*/
namespace CharacterController {

  [RequireComponent(typeof(ThirdPersonCharacter))]
  [RequireComponent(typeof(GameController))]
  //[RequireComponent(typeof(ThirdPersonCameraController))]
  public class InputController : MonoBehaviour
  {
    //Public Vars
    public Camera mainCamera;
    public float speed = 10f;

    //Private Vars
    //private ThirdPersonCameraController cameraController;
    private GameController controller;
    private ThirdPersonCharacter character;
    private Transform cameraTransform;
    private Command moveAxis, buttonSpace, buttonControl, buttonShift, buttonLeftMouse, buttonRightMouse, buttonC, buttonQ;
    private float lastHorz, lastVert;

    //setup the buttons
    private void Start() {
      character = GetComponent<ThirdPersonCharacter>();
      controller = GetComponent<GameController>();
      cameraTransform = mainCamera.transform;

      moveAxis = new Move();
      buttonSpace = new Jump();
      buttonC = new Crouch();
      buttonQ = new Roll();
      buttonShift = new Sprint();
      buttonLeftMouse = new Attack();
      buttonRightMouse = new ADS();

      controller.LockMouse(true);
    }

    private void Update() {
      handleInput();
    }

    private void handleInput() {
      float horz = Input.GetAxisRaw("Horizontal");
      float vert = Input.GetAxisRaw("Vertical");

      //These checks queue up actions such as jump or sprint, then call a move.
      if(Input.GetMouseButton(0)) {
        buttonLeftMouse.Execute(character, cameraTransform);
        controller.LockMouse(true);
      }
      if(Input.GetKeyDown(KeyCode.Escape)) {
        controller.LockMouse(false);
      }
      //buttonLeftMouse.Execute(character, Input.GetMouseButton(0));
      //buttonControl.Execute(Input.GetKey(KeyCode.LeftControl));
      buttonSpace.Execute(Input.GetKey(KeyCode.Space));
      buttonShift.Execute(Input.GetKey(KeyCode.LeftShift));
      buttonRightMouse.Execute(Input.GetMouseButtonDown(1));
      buttonC.Execute(Input.GetKey(KeyCode.C));
      buttonQ.Execute(character, Input.GetKeyDown(KeyCode.Q));

      moveAxis.Execute(character, cameraTransform, new Vector3(horz, 0, vert));

      lastHorz = horz;
      lastVert = vert;
    }

  }
}
