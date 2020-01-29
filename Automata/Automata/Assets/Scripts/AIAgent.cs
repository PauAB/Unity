using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public delegate void stateFunction();
    public Dictionary<string, stateFunction> states;

    protected stateFunction currentState;

    private void Awake()
    {
        states = new Dictionary<string, stateFunction>();
    }

    public virtual void UpdateAgent()
    {

    }
}