using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterController{

  [RequireComponent(typeof(Rigidbody))]
  [RequireComponent(typeof(CapsuleCollider))]
  [RequireComponent(typeof(Animator))]
  public class ThirdPersonCharacterController : MonoBehaviour
  {
    [SerializeField] float c_MovingTurnSpeed = 360;
    [SerializeField] float c_StationaryTurnSpeed = 180;
    [SerializeField] float c_JumpPower = 12f;
    [Range(1f, 4f)][SerializeField] float c_GravityMultiplier = 2f;
    [SerializeField] float c_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float c_MoveSpeedMultiplier = 1f;
    [SerializeField] float c_AnimSpeedMultiplier = 1f;
    [SerializeField] float c_GroundCheckDistance = 0.1f;

      private Rigidbody c_Rigidbody;
      private Animator c_Animator;
      private CapsuleCollider c_Capsule;

      private Vector3 c_CapsuleCenter;
      private Vector3 c_GroundNormal;

      private float c_OriginalGroundCheck;
      private const float k_Half = 0.5f;
      private float c_TurnAmount;
      private float c_ForwardAmount;
      private float c_CapsuleHeight;

      private bool c_Crouching;
      private bool c_Grounded;
      // Start is called before the first frame update
      void Start()
      {
        c_Animator = GetComponent<Animator>();
  			c_Rigidbody = GetComponent<Rigidbody>();
  			c_Capsule = GetComponent<CapsuleCollider>();

        c_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			  c_OriginalGroundCheck = c_GroundCheckDistance;
      }

      // Update is called once per frame
      void Update()
      {

      }
  }
}
