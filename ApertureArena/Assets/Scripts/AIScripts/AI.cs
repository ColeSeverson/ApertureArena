using UnityEngine;
using AIStates;


public class AI : MonoBehaviour
{
    public bool switchState = false;
    //public float gameTimer;
    //public int seconds = 0;

    public int startHealth= 500;
    public int currHealth;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    bool isDead;
    int attackMove;

    public StateMachine<AI> stateMachine { get; set; }

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
        //Move this stuff to states, that do different things

        if(isDead)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            nav.SetDestination(player.position);
            anim.SetBool("isMoving", true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision!");
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking2", true);
            attackMove = 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*
        if (other.gameObject.name == "Character")
        {
            //"logic" for attack switching
            switch(attackMove)
            {
                case 1:
                    anim.SetBool("isAttacking1", false);
                    anim.SetBool("isAttacking2", true);
                    attackMove = 2;
                    break;

                case 2:
                    anim.SetBool("isAttacking2", false);
                    anim.SetBool("isAttacking3", true);
                    attackMove = 3;
                    break;

                case 3:
                    anim.SetBool("isAttacking3", false);
                    anim.SetBool("isAttacking1", true);
                    attackMove = 1;
                    break;

                default:
                    Debug.Log("this should not have happened");
                    break;
            }
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        /*Debug.Log("exit");
        Debug.Log(other.name);
        anim.SetBool("isAttacking1", false);
        anim.SetBool("isAttacking2", false);
        anim.SetBool("isAttacking3", false);
        anim.SetBool("isMoving", false);*/
    }


    private void OnDeath()
    {
        anim.SetBool("isDead", isDead = true);
        nav.enabled = false;

    }
}
