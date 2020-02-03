using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : AI_Agent
{
    Vector3[] waypoints;
    Transform target;
    public int maxWaypoints = 10;
    int actualWaypoint = 0;

    public float angularVelocity = 1f;
    public float speed = 1f;

    public float halfAngle = 30.0f;
    public float coneDistance = 5.0f;

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

            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z), 0.15f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoints[actualWaypoint], 1.0f);

            // Vision Cone
            Vector3 rightSide = Quaternion.Euler(Vector3.up * halfAngle) * transform.forward * coneDistance;
            Vector3 leftSide = Quaternion.Euler(Vector3.up * -halfAngle) * transform.forward * coneDistance;

            Gizmos.color = Color.red;            

            Gizmos.DrawLine(transform.position, transform.position + transform.forward * coneDistance);
            Gizmos.DrawLine(transform.position, transform.position + rightSide);
            Gizmos.DrawLine(transform.position, transform.position + leftSide);

            Gizmos.DrawLine(transform.position + leftSide, transform.position + transform.forward * coneDistance);
            Gizmos.DrawLine(transform.position + rightSide, transform.position + transform.forward * coneDistance);
        }        
    }

    void idle()
    {
        Debug.Log("Idle");
        Gizmos.color = Color.red;

        if(Input.GetKeyDown(KeyCode.A))
        {
            setState(getState("goto"));
        }
    }

    void goToWaypoint()
    {
        Debug.Log("GoToWapoint");
        Gizmos.color = Color.yellow;        

        maxAngle = Vector3.SignedAngle(transform.forward, waypoints[actualWaypoint] - transform.position, Vector3.up);
        angleToGo = Mathf.Min(angularVelocity, Mathf.Abs(maxAngle));  
        angleToGo *= Mathf.Sign(maxAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + angleToGo,
            transform.rotation.eulerAngles.z);
        
        if (Vector3.Distance(waypoints[actualWaypoint], transform.position) <= coneDistance &&
            Vector3.Angle(transform.forward, waypoints[actualWaypoint]) <= halfAngle)
        {
            //speed = 0.05f;
            transform.position += transform.forward * speed;            
        }        

        if (Vector3.Distance(waypoints[actualWaypoint], transform.position) < 0.1f)
        {
            //speed = 0f;
            setState(getState("nextwp"));
        }
    }

    void goToTarget(Transform target)
    {
        Debug.Log("GoToTarget");        

        maxAngle = Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up);
        angleToGo = Mathf.Min(angularVelocity, Mathf.Abs(maxAngle));
        angleToGo *= Mathf.Sign(maxAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + angleToGo,
            transform.rotation.eulerAngles.z);

        if (Vector3.Distance(target.position, transform.position) <= coneDistance &&
            Vector3.Angle(transform.forward, target.position) <= halfAngle * 2)
        {
            //speed = 0.05f;
            transform.position += transform.forward * speed;
        }

        if (Vector3.Distance(target.position, transform.position) < 0.1f)
        {
            //speed = 0f;
            setState(getState("idle"));
        }
    }

    void calculateNextWaypoint()
    {
        Debug.Log("NextWaypoint");
        Gizmos.color = Color.green;

        if (actualWaypoint == waypoints.Length - 1)
            actualWaypoint = 0;

        actualWaypoint++;
        setState(getState("goto"));
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
