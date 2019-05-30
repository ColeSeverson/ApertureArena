using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Cole Severson
  Written to follow the command pattern.
  Controls the character.
*/
namespace CharacterController {

  public abstract class Command
  {
    //temporary bools
      protected static bool jump, crouch, sprint, mouseLocked;
      private ThirdPersonCharacter character;
      //private ThirdPersonCameraController camera;
      //private Image crossHair;

      public virtual void Execute(ThirdPersonCharacter character, Transform cameraAngle, Vector3 dir){}
      public virtual void Execute(bool boolean){}
      public virtual void Execute(ThirdPersonCharacter character){}
      public virtual void Execute(ThirdPersonCharacter character, Transform t){}
      public virtual void Execute(ThirdPersonCharacter character, bool boolean){}
      public virtual void Execute(){}
  }

  public class Move : Command
  {
    public override void Execute(ThirdPersonCharacter character, Transform cameraTransform, Vector3 dir) {
      Vector3 cameraAngle = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
      Vector3 move = dir.z * cameraAngle + dir.x * cameraTransform.right;

      //Handle 'walking'
      move = sprint ? move.normalized * 0.5f : move.normalized;
      character.Move(move, crouch, jump);
    }
  }
  public class Roll : Command
  {
    public override void Execute(ThirdPersonCharacter character, bool roll) {
      if(roll)
        Debug.Log("Roll");
      character.Roll(roll);
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
  public class Attack : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(ThirdPersonCharacter character, Transform t){
      //Debug.Log("Command-Attack");
      character.Attack(character, t);
      if(!mouseLocked) {
        
      }
    }
  }
  public class ADS : Command
  {
    //really should call something like character.jump();
    //This doesn't work because the default ThirdPersonCharacter uses move for jumping as well
    public override void Execute(bool aiming){
      //Debug.Log("Command-ADS");
      //sprint = sprinting;
    }
  }
}
