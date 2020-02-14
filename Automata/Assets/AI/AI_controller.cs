using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AI_controller : MonoBehaviour
{
   
    public AI_Agent agent;
   
    void Start()
    {
        
    }
    
    void Update()
    {
        //ASSERT
        agent.updateAgent();
    }
}
