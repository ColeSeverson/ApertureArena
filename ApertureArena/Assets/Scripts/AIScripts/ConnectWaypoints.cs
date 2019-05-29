using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConnectWaypoints : MonoBehaviour
{
    [SerializeField]
    protected float _connectivityRadius = 10f;

    List<ConnectWaypoints> _connections;
    float debugDrawRadius = 0.2f;

    public void Start()
    {
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        _connections = new List<ConnectWaypoints>();

        for( int i = 0; i < allWaypoints.Length; i++)
        {
            ConnectWaypoints nextWaypoint = allWaypoints[i].GetComponent<ConnectWaypoints>();

            if (nextWaypoint != null)
            {
                if(Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= _connectivityRadius && nextWaypoint != this)
                {
                    _connections.Add(nextWaypoint);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _connectivityRadius);

    }

    public ConnectWaypoints NextWaypoint(ConnectWaypoints previousWaypoint)
    {
        if(_connections.Count == 0)
        {
            Debug.LogError("Insufficient Waypoints");
            return null;
        }
        else if(_connections.Count == 1 && _connections.Contains(previousWaypoint))
        {
            return previousWaypoint;
        }
        else
        {
            ConnectWaypoints nextWaypoint;
            int nextIndex = 0;

            do
            {
                nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                nextWaypoint = _connections[nextIndex];
            } while (nextWaypoint == previousWaypoint);

            return nextWaypoint;
        }
    }
}
