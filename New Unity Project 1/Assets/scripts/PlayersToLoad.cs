using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayersToLoad : MonoBehaviour {

    int players;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Level")
            players = FindObjectOfType<UpdateCountText>().Players;
    }
    public int GetPlayerToBeInGame()
    {
        return players;
    }
    public void SetPlayerToBeInGame(int PlayerAmount)
    {
         players = PlayerAmount;
    }
}
