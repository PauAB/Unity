using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : AI_Agent
{
    Vector3[] waypoints;
    Color color;
    public Transform target;
    public int maxWaypoints = 10;
    int actualWaypoint = 0;

    public float angularVelocity = 1f;
    public float speed = 1f;    

    public float halfAngle = 30.0f;
    public float coneDistance = 5.0f;

    public float orbitDistance;

    private float maxAngle;
    private float angleToGo;

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
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoints[actualWaypoint], 1.0f);
            

            Gizmos.color = color;
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z), 0.25f);

            // Vision Cone
            Vector3 rightSide = Quaternion.Euler(Vector3.up * halfAngle) * transform.forward * coneDistance;
            Vector3 leftSide = Quaternion.Euler(Vector3.up * -halfAngle) * transform.forward * coneDistance;

            Gizmos.color = Color.red;            

            Gizmos.DrawLine(transform.position, transform.position + transform.forward * coneDistance);
            Gizmos.DrawLine(transform.position, transform.position + rightSide);
            Gizmos.DrawLine(transform.position, transform.position + leftSide);

            Gizmos.DrawLine(transform.position + leftSide, transform.position + transform.forward * coneDistance);
            Gizmos.DrawLine(transform.position + rightSide, transform.position + transform.forward * coneDistance);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * orbitDistance);
        }        
    }

    void idle()
    {
        color = Color.red;

        if(Input.GetKeyDown(KeyCode.A))
        {
            setState(getState("goto"));
        }
    }

    void goToWaypoint()
    {
        color = Color.yellow;
        coneDistance = 15f;
        halfAngle = 30f;

        maxAngle = Vector3.SignedAngle(transform.forward, waypoints[actualWaypoint] - transform.position, Vector3.up);
        angleToGo = Mathf.Min(angularVelocity, Mathf.Abs(maxAngle));  
        angleToGo *= Mathf.Sign(maxAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + angleToGo,
            transform.rotation.eulerAngles.z);

        if (Vector3.Distance(target.position, transform.position) <= coneDistance &&
            Vector3.Angle(transform.forward, target.position - transform.position) <= halfAngle)
        {
            setState(getState("target"));
        }
        else
            transform.position += transform.forward * speed;

        if (Vector3.Distance(waypoints[actualWaypoint], transform.position) < 0.1f)
        {
            setState(getState("nextwp"));
        }
    }

    void goToTarget()
    {
        color = Color.yellow;
        coneDistance = 20f;
        halfAngle = 45f;

        maxAngle = Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up);
        angleToGo = Mathf.Min(angularVelocity, Mathf.Abs(maxAngle));
        angleToGo *= Mathf.Sign(maxAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + angleToGo,
            transform.rotation.eulerAngles.z);

        if (Vector3.Distance(target.position, transform.position) <= coneDistance &&
            Vector3.Angle(transform.forward, target.position - transform.position) <= halfAngle)
        {
            transform.position += transform.forward * speed;
        }
        else
            setState(getState("goto"));

        if (Vector3.Distance(target.position, transform.position) - orbitDistance < 0.1f)
        {
            setState(getState("wait"));            
        }
    }

    void calculateNextWaypoint()
    {
        color = Color.blue;        

        if (actualWaypoint == waypoints.Length - 1)
            actualWaypoint = 0;

        actualWaypoint++;
        setState(getState("goto"));
    }

    void wait()
    {
        color = Color.red;

        float nextState = Random.Range(0f, 100f);

        Debug.Log(nextState);
        
        if (nextState <= 25f)
        {
            setState(getState("idle"));
        }
        else if (nextState >= 26f && nextState <= 50f)
        {
            setState(getState("orbit"));
        }
        else if (nextState >= 51f && nextState <= 100f)
        {
            setState(getState("fight"));
        }
    }

    void orbit()
    {
        color = Color.green;
    }

    void fight()
    {
        color = Color.black;
    }

    void Start()
    {
        initPositions();
        actualWaypoint = 0;        

        initState("idle", idle);
        initState("goto", goToWaypoint);
        initState("nextwp", calculateNextWaypoint);
        initState("target", goToTarget);
        initState("wait", wait);
        initState("orbit", orbit);
        initState("fight", fight);
        
        setState(getState("idle"));
    }
    
    void Update()
    {
        
    }
}
