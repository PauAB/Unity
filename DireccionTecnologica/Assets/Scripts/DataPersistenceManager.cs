using UnityEngine;  
using System.IO;

public static class DataPersistenceManager
{
    public static void SaveJson(Player data, string jsonPath)
    {       
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(jsonPath, jsonData);
    }

    public static T LoadJson <T>(string jsonPath)
    {
        T data = JsonUtility.FromJson<T>(File.ReadAllText(jsonPath));

        return data;
    }    

    public static void SaveJsonArray(Player[] data, string jsonPath)
    {        
        string jsonData = JsonHelper.ArrayToJson(data);
        File.WriteAllText(jsonPath, jsonData);
    }

    public static T[] LoadJsonArray <T>(string jsonPath)
    {
        string jsonData = JsonUtility.FromJson<string>(File.ReadAllText(jsonPath));
        T[] data = JsonHelper.GetJsonArray<T>(jsonData);

        return data;
    }
}
