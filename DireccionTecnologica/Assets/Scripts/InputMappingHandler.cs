using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class InputMappingHandler : ScriptableWizard
{
    public KeyCode code;
    public Command cmd;
    
    // Start is called before the first frame update
    [MenuItem("Input/Input Binding...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Input Map", typeof(InputMappingHandler), "Bind");
    }

    private void OnWizardCreate()
    {
        
        
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
