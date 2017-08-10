using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Transform[] spawnpoints;
    public Rigidbody2D Gravestone;

    PlayerController[] playersTemp;
    public PlayerController[] players;
    public int MaxKills = 5;
	// Use this for initialization
	void Start () {
        players = new PlayerController[4];
        playersTemp = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < playersTemp.Length; i++)
        {
            players[playersTemp[i].Identifier - 1] = playersTemp[i];
        }
    }

    // Update is called once per frame
    void Update() {
        
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                if (players[i].Health <= 0)
                {
                    Vector3 deathLoc = players[i].gameObject.transform.position;
                    Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                    players[i].gameObject.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
                    players[i].Health = 1;
                }
            }
        }
    }
}
