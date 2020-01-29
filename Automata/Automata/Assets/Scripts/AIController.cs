using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    
    public AIAgent agent;    

    void Start()
    {

    }

    void Update()
    {
        // assert
        agent.UpdateAgent();
    }
}