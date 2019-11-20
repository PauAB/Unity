using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour
{ 
    Dictionary<string, ConsoleFunction> consoleFunctions;

    ChatWindow consolePrefab;
    const char separator = ' ';
    const char commandStarter = '/';
    public static ConsoleManager instance;

  
    // Start is called before the first frame update
    void Start()
    {
        consolePrefab = FindObjectOfType<ChatWindow>();
        //ConsoleFunction orcSpawn = ;
        consoleFunctions = new Dictionary<string, ConsoleFunction>();
        consoleFunctions.Add(nameof(SpawnOrc), new SpawnOrc(3, ConsoleFunction.CONSOLE_DATATYPE.NUMERIC, ConsoleFunction.CONSOLE_DATATYPE.NUMERIC, ConsoleFunction.CONSOLE_DATATYPE.NUMERIC));

        string mssg = "/SpawnOrc 10 40 50";
      
        DispatchMessage(mssg);

    }

    public void DispatchMessage(string mssg)
    {
        if (consolePrefab != null)
        {
            consolePrefab.SendMessageToChat(mssg, ChatWindow.MESSAGETYPE.REGULAR);
            if (mssg[0] == commandStarter)
            {
                mssg = mssg.Substring(1);

                string[] messageContainer = mssg.Split(separator);
                string[] parameters = new string[messageContainer.Length - 1];
                if (messageContainer[0] == "help")
                {
                    foreach (ConsoleFunction cF in consoleFunctions.Values)
                    {
                        consolePrefab.SendMessageToChat("Function " + cF.GetType().Name + " " + cF.FunctionArgumentsDenition(), ChatWindow.MESSAGETYPE.HELP);


                    }
                    return;
                }
                System.Array.Copy(messageContainer, 1, parameters, 0, messageContainer.Length - 1);

                if (consoleFunctions.ContainsKey(messageContainer[0]))
                {
                    if (!consoleFunctions[messageContainer[0]].Execute(parameters))
                    {
                        consolePrefab.SendMessageToChat("Wrong number of arguments, function expects " + consoleFunctions[messageContainer[0]].FunctionArgumentsDenition(), ChatWindow.MESSAGETYPE.DANGER);
                        Debug.LogWarning("Wrong number of arguments, function expects " + consoleFunctions[messageContainer[0]].FunctionArgumentsDenition());
                    }
                }
                else
                {
                    consolePrefab.SendMessageToChat("No function data on found by the name of: " + messageContainer[0] + "Type /help to see availables functions", ChatWindow.MESSAGETYPE.DANGER);

                    Debug.LogWarning("No function data on found by the name of: " + messageContainer[0] + "Type /help to see availables functions");
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
