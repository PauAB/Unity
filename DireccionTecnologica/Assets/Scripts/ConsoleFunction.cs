using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public abstract class ConsoleFunction 
{
    /*
     * 
     * ConsoleFunction(int params) //Parametros que vamos a pasar.
     */
    public abstract bool Execute(params object[] parameters);

    public abstract string FunctionArgumentsDefinition();

    
    void helpWithMethods(string myMethod)
    {

    }

    protected void helpWithMethods()
    {

        
        foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            foreach(ParameterInfo info in method.GetParameters())
            {
               
                Debug.Log(method.Name + " : " + info.Name + " TypeOf: ("+ info.ParameterType +")");
                
            }
        }
    }
}