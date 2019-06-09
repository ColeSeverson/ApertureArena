using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//This code is taken from the Survival Shooter Unity tutorial, we did this project for our assignment 2 so the schema of this set up was familiar to me.

namespace EnemyInformation
{
    public class EnemyHealth : MonoBehaviour
    {
        public  int startingHealth = 10;            // The amount of health the enemy starts the game with.
        private int currentHealth;                // The current health the enemy has.
        public AudioClip deathClip;
        public AudioClip hitClip;            
        public RectTransform healthbar;

        Animator anim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;
        bool takingDamage;                             // Whether the enemy is dead.

        void Awake()
        {
            // Setting up the references for use later.
            anim = GetComponent<Animator>();

            enemyAudio = GetComponent<AudioSource>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            enemyAudio.GetComponent<AudioSource>();
            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }

        IEnumerator IFrames(){
          yield return new WaitForSeconds(1f);
          takingDamage = false;
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {

            anim.SetBool("isAlerted", true);

            // If the enemy is dead...
            if (isDead || takingDamage)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect. We havent implemented this yet but will in the future
            enemyAudio.clip = hitClip;
            enemyAudio.volume = 0.25f;
            enemyAudio.Play();


            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;
            StartCoroutine(IFrames());

            // If the current health is less than or equal to zero...
            if (currentHealth <= 0)
            {
                // using a function to be able to add functionality later and keep the logic contained
                Death();
            }

            healthbar.sizeDelta = new Vector2(currentHealth * (200 / startingHealth), healthbar.sizeDelta.y);
        }


        void Death()
        {
            // The enemy is dead. This tells the script.
            isDead = true;

            // Tell the animator that the enemy is dead.
            enemyAudio.clip = deathClip;
            enemyAudio.volume = 0.05f;
            enemyAudio.Play();
            anim.SetBool("isDead", true);
            Destroy(gameObject, 2f);

        }
    }
}
