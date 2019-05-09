using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController {
  enum moveState
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
      protected static bool jump, crouch, sprint;
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

    //  Debug.Log("Command-Move");

      //add Sprinting
      Vector3 cameraAngle = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
      Vector3 move = dir.z * cameraAngle + dir.x * cameraTransform.right;

      Debug.Log(move.ToString());
      move = sprint ? move.normalized : move.normalized * 0.5f;


      character.Move(move, crouch, jump);
    }
  }
  public class Jump : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool jumping){
    //  Debug.Log("Command-Jump");
      jump = jumping;
    }
  }
  public class Crouch : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool crouching){
    //  Debug.Log("Command-Crouch");
      crouch = crouching;
    }
  }
  public class Sprint : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool sprinting){
    //  Debug.Log("Command-Sprint");
      sprint = sprinting;
    }
  }
}
