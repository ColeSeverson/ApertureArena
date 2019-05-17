using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Cole Severson
  Basic parent class so that the ThirdPersonCharacterController doesn't have to worry about weapon type
*/
namespace CharacterController {
  public class Weapon : MonoBehaviour
  {
    public virtual void Execute(){
      Debug.Log("Default Execute");
    }
  }
}
