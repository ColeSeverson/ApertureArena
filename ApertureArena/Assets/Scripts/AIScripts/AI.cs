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

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking2", true);
        }
    }


    private void OnDeath()
    {
        anim.SetBool("isMoving", false);
        nav.enabled = false;
        Destroy(gameObject, 2f);

    }
}
