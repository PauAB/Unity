using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Diagnostics;
public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    public delegate void ExecuteFunction();
    
    Dictionary<KeyCode, Command> inputs;
    protected virtual void Awake()
    {
        StackFrame callStack = new StackFrame(1, true);
        Assert.IsTrue(FindObjectsOfType<InputManager>().Length < 2,
          callStack.GetFileLineNumber() + callStack.GetFileName()+ "GameManager has more than 1 copy. It's a singleton Class and there can only be one GameManager for each game");
        Assert.raiseExceptions = true;
       // __LINE__ __FILE__
        instance = this;
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //    Destroy(this);

        inputs = new Dictionary<KeyCode, Command>();
    }

    public static void SetInputs(KeyCode code, Command func)
    {
      
    }

    private void Update()
    {
   
    }
    

    

}
