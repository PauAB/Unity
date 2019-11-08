using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ConsoleManager : MonoBehaviour
{
    Dictionary<string, ConsoleFunction> consoleFunctions;

    const char separator = ' ';    
    string[] messageContainer;
    string[] messageFuncName;
    string[] messageParams;    
    
    void Start()
    {
        consoleFunctions = new Dictionary<string, ConsoleFunction>();
        consoleFunctions.Add(nameof(SpawnOrc), new SpawnOrc());
        DispatchMessage("/SpawnOrc 1 0 0");
    }

    void DispatchMessage(string mssg)
    {
        messageContainer = mssg.Split(separator);

        if (messageContainer[0].StartsWith("/"))
        {
            messageFuncName = messageContainer[0].Split('/');            

            if (messageFuncName[1] != "")
            {
                if (messageFuncName[1] == "help")
                {
                    foreach (ConsoleFunction cF in consoleFunctions.Values)
                    {
                        Debug.Log("Function " + cF.GetType().Name + " " + cF.FunctionArgumentsDefinition());
                    }

                    return;
                }
                else
                {
                    foreach (ConsoleFunction cF in consoleFunctions.Values)
                    {
                        if (messageFuncName[1] == cF.GetType().Name)
                        {                            
                            Debug.Log("With params: " + cF.Execute(messageContainer[1], messageContainer[2], messageContainer[3]));
                            Debug.Log("With params: " + cF.Execute());
                        }
                    }
                }
            }
            else Debug.Log("Invalid function");
        }
        else Debug.Log("Invalid function");
    }

}
