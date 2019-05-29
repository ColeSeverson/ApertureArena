using UnityEngine;
using AIStates;

//This is a pretty simple driver for our AI, elements of it are fomr the Survival Shooter Unity Tutorial
// Patrolling State Functionality
// Much of this is from https://www.youtube.com/watch?v=8_zTQsYFwf0&t=4s
// This is just a general tutorial on how to get an AI to patrol a given area, I used parts of this, but the overall 
// implementation is my own

public class AI : MonoBehaviour
{

    public float sightDistance = 50f;
    public int startHealth = 500;
    public int currHealth;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;

    [SerializeField]
    bool _patrolWaiting;

    [SerializeField]
    float _totalWaitTime = 3f;

    ConnectWaypoints _currentWaypoint;
    ConnectWaypoints _previousWaypoint;

    bool _alerted = false;
    bool _travelling = false;
    bool _waiting;
    float _waitTimer;
    int _waypointsVisited;
    bool _sunk = false;

    public void Awake()
    {
        nav = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Player").transform != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        anim = GetComponent<Animator>();
        currHealth = startHealth;

        if (nav == null)
        {
            Debug.LogError("This object has no nav mesh");
        }
        else
        {
            if (_currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWaypoints.Length > 0)
                {
                    while (_currentWaypoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                        ConnectWaypoints startingWaypoint = allWaypoints[random].GetComponent<ConnectWaypoints>();

                        if (startingWaypoint != null)
                        {
                            _currentWaypoint = startingWaypoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("No available waypoints");
                }
            }
            SetDestination();

        }
    }

    private void Update()
    { 
        if (anim.GetBool("isDead"))
        {
            OnDeath();
        }
        else if (CanSeeEnemy() || _alerted)
        {
            Alerted();
        }
        else if (_travelling && nav.remainingDistance <= 1.0f)
        {
            _travelling = false;
            _waypointsVisited++;

            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                SetDestination();
            }
        }

        if (_waiting && !_alerted)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                SetDestination();
            }
        }

    }

    private void Alerted()
    {
        if(Vector3.Distance(transform.position, player.position) < 1)
        {
            anim.SetBool("isMoving", false);
            nav.speed = 0;
            anim.SetTrigger("Attack");
        }
        else
        {
            nav.SetDestination(player.position);
            anim.ResetTrigger("Attack");
            nav.speed = 2;
            anim.SetBool("isMoving", true);

        }
    }

    // Destroys the game object and 
    private void OnDeath()
    {
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking2", false);
        anim.ResetTrigger("Attack");

        if (!_sunk)
        {
            _sunk = true;
            transform.Translate(0f, 0.2f, 0);
        }

        nav.enabled = false;
        Destroy(gameObject, 2f);
    }

    private void SetDestination()
    {
        if (_waypointsVisited > 0)
        {
            ConnectWaypoints nextWaypoint = _currentWaypoint.NextWaypoint(_previousWaypoint);
            _previousWaypoint = _currentWaypoint;
            _currentWaypoint = nextWaypoint;
        }

        Vector3 targetVector = _currentWaypoint.transform.position;
        anim.SetBool("isMoving", true);

        nav.SetDestination(targetVector);
        _travelling = true;

    }

    private bool CanSeeEnemy()
    {
        if (Vector3.Distance(player.position, this.transform.position) <= sightDistance)
        {
            if (Vector3.Dot(this.transform.forward, player.position) > 0 && Vector3.Angle(this.transform.forward, player.position) < 60)
            {
                Vector3 playerOffset = new Vector3(0, 0.5f, 0);
                Vector3 aiOffset = new Vector3(0, 1f, 0);

                if (Physics.Raycast(this.transform.position + aiOffset, (player.transform.position + playerOffset) - this.transform.position, out RaycastHit hit))
                {
                    Debug.DrawRay(this.transform.position + aiOffset, (player.transform.position + playerOffset) - this.transform.position, Color.red);
                    Debug.Log(hit.collider.tag);

                    if (hit.collider.tag == "Player")
                    {
                        _alerted = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }
        return false;
    }
}
