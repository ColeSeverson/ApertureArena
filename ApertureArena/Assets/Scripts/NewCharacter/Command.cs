using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController {
  enum charState
  {
    Standing,
    Airborn,
    Dying,
    Crouching,
    Jumping,
    Sprinting
  }

  public abstract class Command
  {
    //temporary bools
      public bool jump, crouch, sprint;
      private ThirdPersonCharacter character;
      //private ThirdPersonCameraController camera;
      //private Image crossHair;

      public virtual void Execute(ThirdPersonCharacter character, Transform cameraAngle, Vector3 dir){}
      public virtual void Execute(bool boolean){}
  }

  public class Move : Command
  {
    public override void Execute(ThirdPersonCharacter character, Transform cameraTransform, Vector3 dir) {
      //bool jump = if charState == Jumping ? true : false;
      //bool crouching = if charState == Crouching ? true : false;

      //add Sprinting
      Vector3 cameraAngle = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
      Vector3 move = dir.z * cameraAngle + dir.y * cameraTransform.right;

      move = sprint ? move : move * 0.5f;

      character.Move(move, crouch, jump);
    }
  }
  public class Jump : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool jumping){
      jump = jumping;
    }
  }
  public class Crouch : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool crouching){
      crouch = crouching;
    }
  }
  public class Sprint : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool sprinting){
      sprint = sprinting;
    }
  }
}
