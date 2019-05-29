using UnityEngine;
using UnityEngine.AI;
using AIStates;
using System.Collections.Generic;

//Patrolling State
// Much of this is from https://www.youtube.com/watch?v=8_zTQsYFwf0&t=4s
// This is just a general tutorial on how to get an AI to patrol a given area, I used parts of this, but the overall 
// implementation is my own
public class FirstState : MonoBehaviour
{

    [SerializeField]
    bool _patrolWaiting;

    [SerializeField]
    float _totalWaitTime = 3f;

    [SerializeField]
    float switchProbability = 0.2f;

    ConnectWaypoints _currentWaypoint;
    ConnectWaypoints _previousWaypoint;

    NavMeshAgent _nav;
    bool _travelling;
    bool _waiting;
    float _waitTimer;
    int _waypointsVisited;

    public void Start()
    {
       _nav = this.GetComponent<NavMeshAgent>();

        if (_nav == null)
        {
            Debug.LogError("This object has no nav mesh");
        }
        else
        {
            if(_currentWaypoint == null)
            {
                GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if(allWaypoints.Length > 0)
                {
                    while(_currentWaypoint == null)
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

    public void Update()
    {
        if (_travelling && _nav.remainingDistance <= 1.0f)
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

        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                SetDestination();
            }
        }
        
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
        _nav.SetDestination(targetVector);
        _travelling = true;

    }
    /*

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }  

    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new FirstState();
            }

            return _instance;
        }
    }
        
    public override void EnterState(AI owner)
    {
        Debug.Log("Entering First State");
        // We can do stuff in here
    }

    public override void ExitState(AI owner)
    {
        Debug.Log("Exiting First State");
    }

    public override void UpdateState(AI owner)
    {
        if (owner.switchState)
        {
            owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
    */
}
