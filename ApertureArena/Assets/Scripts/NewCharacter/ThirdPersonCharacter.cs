/*
 * Cole Severson
 * This code is based off of the UNity Essentials thirdpersoncharacter controller.
 	 Adapted to fit our needs for a character while keeping the animator controller code
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CharacterController
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{

		//Serialized fields inlcuded for the animations
		[SerializeField] float m_MovingTurnSpeed = 9999;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 15f;
		[SerializeField] float m_GravityMultiplier = 1f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		//serialized fields for added code
		[SerializeField] float c_BlinkDistance = .25f;
		[SerializeField] float c_IFrameDuration = 1f;
		[SerializeField] float c_RollDelay = 1f;

		public Text deathText;


		//componenets included in the Avatar
		Rigidbody m_Rigidbody;
		Animator m_Animator;
		CapsuleCollider m_Capsule;
		SkinnedMeshRenderer c_Mesh;
		ParticleSystem c_Particles;
	//	GameController c_Controller;

		Vector3 c_CurrentMove;
		Vector3 m_GroundNormal;
		Vector3 m_CapsuleCenter;
		Weapon c_Weapon;


		float m_OrigGroundCheckDistance;
		float m_TurnAmount;
		float m_ForwardAmount;
		float m_CapsuleHeight;
		float c_CapsuleRadius;
		float c_GroundCheckDistance = 0.1f;
		const float k_Half = 0.5f;

		int c_Health;

		bool c_RollCooldown;
		bool m_Crouching;
		bool m_IsGrounded;
		bool c_Blinking;
		bool c_Attacking;
		bool c_Dying;
		bool c_Jumping;
		bool c_Rolling;

		//Code to set up the private variables
		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			c_Mesh = GetComponentsInChildren<SkinnedMeshRenderer>()[0];
			c_Weapon = GetComponentsInChildren<Weapon>()[0];
			c_Particles = GetComponent<ParticleSystem>();
			//c_Controller = GetComponent<GameController>();

			c_Particles.Stop();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
			c_CapsuleRadius = m_Capsule.radius;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;

			c_Health = 100;
		}
		void FixedUpdate(){
			if (c_Jumping && !m_Crouching && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				// jump!
				m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
				m_IsGrounded = false;
				m_Animator.applyRootMotion = false;
				m_GroundCheckDistance = 0.1f;
			}
			if(!m_IsGrounded) {
				Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier * 200f) - Physics.gravity;
				extraGravityForce *= Time.fixedDeltaTime;
				m_Rigidbody.AddForce(extraGravityForce);

				m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
			}
		}
		//LateUpdate just checks for dying
		void LateUpdate(){
			if (c_Health <= 0 && c_Dying == false) {
				c_Dying = true;
				c_Particles.Play();
				c_Particles.Emit(100);
				deathText.text = "You have died.";
				c_Weapon.Destroy();
				StartCoroutine(Dying());
			}
		}
		void OnTriggerEnter(Collider col) {
			if (col.gameObject.tag == "Spear") {
				c_Health -= 40;
			}
		}
		IEnumerator Dying() {
			yield return new WaitForSeconds(.2f);
			c_Mesh.enabled = false;
		}
		public bool isDead() {
			return c_Dying;
		}
		/*IEnumerator IFrames(){
			c_Mesh.enabled = false;
			c_Blinking = true;
			yield return new WaitForSeconds(c_IFrameDuration);
			//transform.position = transform.position + (c_CurrentMove.normalized * c_BlinkDistance);
			//transform.position = transform.position + new Vector3(0f, 1f, 0f);
			c_Mesh.enabled = true;
			c_Blinking = false;
		}

		public void Blink(){
			//dissapear -> enable iFrames -> create particle effects -> move -> reeapear
			Debug.Log("Blink");
			StartCoroutine(IFrames());
		}*/
		IEnumerator Rolling(){
			yield return new WaitForSeconds(.4f);
			c_Rolling = false;

			yield return new WaitForSeconds(c_RollDelay - .4f);
			c_RollCooldown = false;
		}
		/*IEnumerator RollCooldown(){
			yield return new WaitForSeconds(1f);
			c_RollCooldown = false;
		}*/
		public void Roll(){
			if(m_IsGrounded && !c_Attacking && !c_RollCooldown) {
					m_Animator.SetBool("Rolling", true);
			//	if(roll) {
					c_Rolling = true;
					c_RollCooldown = true;
					StartCoroutine(Rolling());
					//StartCoroutine(RollCooldown());
			//	}
			}else {
				m_Animator.SetBool("Rolling", false);
			}
		}
		//This move code is based off of the ThirdPersonCharacter Unity essential asset
		//Used to cause movement instead of checking every frame
		public void Move(Vector3 move, bool crouch, bool jump)
		{
			c_Jumping = jump;
			if (c_Blinking || c_Attacking || c_Dying) {
				//UpdateAnimator(new Vector3(0, 0, 0));
				return;
			}

		//	Debug.Log(move.ToString());
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f)
				move.Normalize();

			//Check the new Ground Status
			CheckGroundStatus();

			//Changes the move to relative vectors, and projects it on the plane of the ground
			move = transform.InverseTransformDirection(move);
			c_CurrentMove = move;
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);

			//Get the amount we will need to turn and the forward amount
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;
			transform.Rotate(0, m_TurnAmount * 1000 * Time.deltaTime, 0);


			// control and velocity handling is different when grounded and airborne:
			if (!m_IsGrounded)
			{
				//HandleAirborneMovement(move);
			}

			//This code causes a crouch or leeps you crouched if you are already and under something
			ScaleCapsuleForCrouching(crouch || c_Rolling);
			PreventStandingInLowHeadroom();

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}

		void HandleAirborneMovement(Vector3 move)
		{
			//Allow airborne movement
		//	Vector3 airMove = new Vector3(move.x*6f, m_Rigidbody.velocity.y, move.z*6f);
		//	m_Rigidbody.velocity = Vector3.Lerp(m_Rigidbody.velocity, airMove, Time.deltaTime*2f);

			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			m_Rigidbody.AddForce(extraGravityForce);

			m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}

		//This code is purely from essential assets
		void ScaleCapsuleForCrouching(bool crouch)
		{
			if (m_IsGrounded && crouch)
			{
				if (m_Crouching) return;
				//Debug.Log("Trying");
				m_Capsule.height = m_Capsule.height / 2f;
				m_Capsule.center = m_Capsule.center / 2f;
				m_Crouching = true;
			}
			else
			{
				Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
				float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
				if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					m_Crouching = true;
					return;
				}
				m_Capsule.height = m_CapsuleHeight;
				m_Capsule.center = m_CapsuleCenter;
				m_Crouching = false;
			}
		}

		void PreventStandingInLowHeadroom()
		{
			// prevent standing up in crouch-only zones
			if (!m_Crouching)
			{
				Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
				float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
				if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					m_Crouching = true;
				}
			}
		}

		//This code was included in the default assets to control the characters animation cycle
		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("Crouch", m_Crouching);
			m_Animator.SetBool("OnGround", m_IsGrounded);
			if (!m_IsGrounded)
			{
				m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
			}

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				m_Animator.SetFloat("JumpLeg", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (m_IsGrounded && move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				// don't use that while airborne
				m_Animator.speed = 1;
			}
		}

		//Causes an attack using the current weapons
		IEnumerator Attacking(){
				yield return new WaitForSeconds(.3f);
				m_Animator.SetBool("Attacking", false);
				c_Attacking = false;
		}
		public void Attack(bool attack, Transform cameraAngle){
			if(c_Dying) return;
			if(m_Animator.GetBool("OnGround")){
				m_Animator.SetBool("Attacking", attack);
				c_Attacking = attack;
				StartCoroutine(Attacking());
				//now we do all of our gun stuff!
				//float timer = 0f;
				Vector3 toFace = Vector3.Scale(cameraAngle.forward, new Vector3(1, 0, 1)).normalized;
				transform.forward = toFace;
				c_Weapon.Execute();

			}
		}

		//This is the code that causes motion based off of the animations
		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.

				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			} else {
				//IE we are airborn
				Vector3 move = c_CurrentMove.normalized;

				Vector3 dir = transform.InverseTransformDirection(move).normalized;
				Vector3 v = (dir * m_MoveSpeedMultiplier * 5f);
				v.x = -v.x;

				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + new Vector3(c_CapsuleRadius - c_GroundCheckDistance, 0, c_CapsuleRadius - c_GroundCheckDistance) + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance) ||
				Physics.Raycast(transform.position + new Vector3(-c_CapsuleRadius + c_GroundCheckDistance, 0, c_CapsuleRadius - c_GroundCheckDistance) + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance) ||
				Physics.Raycast(transform.position + new Vector3(c_CapsuleRadius - c_GroundCheckDistance, 0, -c_CapsuleRadius + c_GroundCheckDistance) + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance) ||
				Physics.Raycast(transform.position + new Vector3(-c_CapsuleRadius + c_GroundCheckDistance, 0, -c_CapsuleRadius + c_GroundCheckDistance) + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance) ||
				Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				m_Animator.applyRootMotion = false;
			}
		}
	}
}
