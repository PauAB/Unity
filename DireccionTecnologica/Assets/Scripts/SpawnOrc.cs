using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrc : ConsoleFunction
{
    string paramDefinition = "takes 3 parameters";
    
    public override bool Execute(params object[] parameters)
    {        
        if (parameters.GetLength(0) <= 0) return false;
        else if (parameters.GetLength(0) > 0) return true;

        return false;
    }

    public override string FunctionArgumentsDefinition()
    {
        return paramDefinition;
    }

   
}
