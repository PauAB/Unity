using UnityEngine;

public class Player
{
    
    public string playerName;    
    public float playerPower;   
    public float playerStrength;

    public void SetPlayer(string name, float power, float strength)
    {
        playerName = name;
        playerPower = power;
        playerStrength = strength;
    }
}
