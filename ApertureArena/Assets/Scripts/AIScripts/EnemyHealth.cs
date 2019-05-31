using UnityEngine;

//This code is taken from the Survival Shooter Unity tutorial, we did this project for our assignment 2 so the schema of this set up was familiar to me.

namespace EnemyInformation
{
    public class EnemyHealth : MonoBehaviour
    {
        public int startingHealth = 500;            // The amount of health the enemy starts the game with.
        int currentHealth;                   // The current health the enemy has.
        public AudioClip deathClip;                 // The sound to play when the enemy dies.

        Animator anim;                              // Reference to the animator.
        AudioSource enemyAudio;                     // Reference to the audio source.
        CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
        bool isDead;                                // Whether the enemy is dead.

        void Awake()
        {
            // Setting up the references for use later.
            anim = GetComponent<Animator>();
            enemyAudio = GetComponent<AudioSource>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            // Setting the current health when the enemy first spawns.
            currentHealth = startingHealth;
        }

        public void TakeDamage(int amount, Vector3 hitPoint)
        {
 
            // If the enemy is dead...
            if (isDead)
                // ... no need to take damage so exit the function.
                return;

            // Play the hurt sound effect. We havent implemented this yet but will in the future
            //enemyAudio.Play();

            // Reduce the current health by the amount of damage sustained.
            currentHealth -= amount;

            // If the current health is less than or equal to zero...
            if (currentHealth <= 0)
            {
                // using a function to be able to add functionality later and keep the logic contained
                Death();
            }
        }


        void Death()
        {
            // The enemy is dead. This tells the script.
            isDead = true;

            // Tell the animator that the enemy is dead.
            anim.SetBool("isDead", true);

        }
    }
}
