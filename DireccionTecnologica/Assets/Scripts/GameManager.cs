using UnityEngine;

public class GameManager : MonoBehaviour
{

    int maxPlayers = 5;

    Player player;
    Player player2;
    Player player3;
    Player[] players;

    [SerializeField]
    string jsonString;

    void Start()
    {
        player = new Player();
        player2 = new Player();
        player3 = new Player();

        players = new Player[maxPlayers];

        player.SetPlayer("Player 1", 10f, 5f);
        player2.SetPlayer("Player 2", 5f, 2.5f);
        player3.SetPlayer("Player 3", 20f, 10f);

        players[0] = player;
        players[1] = player2;
        players[2] = player3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DataPersistenceManager.SaveJson(player, Application.dataPath + "/playerJson.json");
            Debug.Log("Saved");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            DataPersistenceManager.LoadJson<Player>(Application.dataPath + "/playerJson.json");
            Debug.Log("Loaded");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            DataPersistenceManager.SaveJsonArray(players, Application.dataPath + "/playersJson.json");
            Debug.Log("Array Saved");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            DataPersistenceManager.LoadJsonArray<Player>(Application.dataPath + "/playersJson.json");
            Debug.Log("Array Loaded");
        }
    }
}
