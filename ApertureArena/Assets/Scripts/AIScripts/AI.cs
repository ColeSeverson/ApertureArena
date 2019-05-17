using UnityEngine;
using AIStates;


//This is a pretty simple driver for our AI, elements of it are fomr the Survival Shooter Unity Tutorial

public class AI : MonoBehaviour
{
    public bool switchState = false;
    //public float gameTimer;
    //public int seconds = 0;

    public int startHealth = 500;
    public int currHealth;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;


    public StateMachine<AI> stateMachine { get; set; } // will be used later for now it's not used

    private void Awake()
    {
        //stateMachine = new StateMachine<AI>(this);
        //stateMachine.ChangeState(FirstState.Instance);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        currHealth = startHealth;
    }

    private void Update()
    {
        if(anim.GetBool("isDead"))
        {
            OnDeath();
        }
        else
        {
            nav.SetDestination(player.position);
            anim.SetBool("isMoving", true);
        }

    }

    // Set the animator to be doing the attack move based on a 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking2", true);
        }
    }

    // Destroys the game object and 
    private void OnDeath()
    {
        anim.SetBool("isMoving", false);
        nav.enabled = false;
        Destroy(gameObject, 2f);

    }
}
