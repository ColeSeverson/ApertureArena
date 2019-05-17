using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController {
  public class Weapon : MonoBehaviour
  {
    public virtual void Execute(){
      Debug.Log("Default Execute");
    }
  }
}
