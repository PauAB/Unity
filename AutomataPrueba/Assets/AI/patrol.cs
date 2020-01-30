using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : AI_Agent
{
    Vector3[] waypoints;
    Transform target;
    public int maxWaypoints = 10;
    int actualWaypoint = 0;

    public float angularVelocity = 0.1f;
    public float speed = 1f;

    void initPositions()
    {
        List<Vector3> waypointsList = new List<Vector3>();
        float anglePartition = 360.0f / (float)maxWaypoints;
        for (int i = 0; i < maxWaypoints; ++i)
        {
            Vector3 v = transform.position + 5 *  Vector3.forward * Mathf.Cos(i* anglePartition) 
                + 5* Vector3.right * Mathf.Sin(i*anglePartition);
            waypointsList.Add(v);

        }
        waypoints = waypointsList.ToArray();
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            for (int i = 0; i < maxWaypoints; i++)
            {
                Gizmos.DrawSphere(waypoints[i], 1.0f);
            }
        }
    }

    void idle()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            setState(getState("goto")) ;
        }
    }

    void goToWaypoint()
    {
        Debug.Log("Moving to waypoint");

        float maxAngle = Vector3.SignedAngle(Vector3.forward, waypoints[actualWaypoint] - transform.position, Vector3.up);
        float angleToGo = Mathf.Min(angularVelocity * Mathf.Sign(maxAngle), maxAngle);

        transform.rotation = Quaternion.Euler(Vector3.up * angleToGo);
        
        //float angleToGo = Mathf.Min(angularVelocity, Vector3.SignedAngle(Vector3.forward, waypoints[actualWaypoint] - transform.position, Vector3.up));
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + angleToGo, transform.rotation.eulerAngles.z));

        //transform.position += Vector3.forward * speed;

        //if ((waypoints[actualWaypoint] - transform.position).x <= 0.1)
        //{
        //    setState(getState("nextwp"));
        //}
    }

    void calculateNextWaypoint()
    {
        actualWaypoint++;
    }
    
    void Start()
    {
        initPositions();
        actualWaypoint = 0;        

        initState("idle", idle);
        initState("goto", goToWaypoint);
        initState("nextwp", calculateNextWaypoint);
        
        setState(getState("idle"));
    }
    
    void Update()
    {
        
    }
}
