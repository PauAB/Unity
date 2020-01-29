using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : AIAgent
{
    Vector3[] waypoints;
    Transform target;    

    public int maxWaypoints = 10;

    int currentWaypoint = 0;

    void InitPositions()
    {
        List<Vector3> waypointsList = new List<Vector3>();
        int anglePartition = 360 / maxWaypoints;

        for (int i = 0; i < maxWaypoints; i++)
        {
            Vector3 v = transform.position + 5 * Vector3.forward * Mathf.Cos(anglePartition) + Vector3.right * Mathf.Sin(anglePartition);
            waypointsList.Add(v);
        }

        waypoints = waypointsList.ToArray();
    }
   
    stateFunction GetState(string stateName)
    {
        if (states.ContainsKey(stateName))
        {
            return states[stateName];
        }
        else
        {
            // assert
            return null;
        }        
    }

    void Idle()
    {
        Debug.Log("Idle");

        if (Input.GetKeyDown(KeyCode.N))
        {
            currentState = GetState("goto");
        }
    }

    void GoToWaypoint()
    {

    }

    void CalculateNextWaypoint()
    {

    }

    // ----------------------------------------------------------
    //states["lambda"] = () => { Debug.Log("Lambda Function"); }; // Lambda Function Example
    // ----------------------------------------------------------

    // Reset Waypoint
    //(currentWaypoint++) % waypoints.Length;

    void Start()
    {
        
        InitPositions();
        currentWaypoint = 0;
        states["idle"] = Idle;
        states["goto"] = GoToWaypoint;
        states["nextwp"] = CalculateNextWaypoint;        

        currentState = states["idle"];        
    }

    void Update()
    {
        
    }
}
