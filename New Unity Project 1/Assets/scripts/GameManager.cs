using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Transform[] spawnpoints;
    public Rigidbody2D Gravestone;
    public GameObject deathEffect;
    PlayerController[] playersTemp;
    public PlayerController[] players;
    public int MaxKills = 5;
    public bool GameIsOn;
	// Use this for initialization
	void Start () {
        GameIsOn = true;
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
            if (players[i] != null && GameIsOn)
            {
                if (players[i].GetComponent<Health>().currentHealth <= 0.001)
                {
                    KillPlayer(i);
                    if(players[i].playerWhoShotMe != null)
                        players[i].playerWhoShotMe.KillCount++;
                    players[i].playerWhoShotMe = null;
                    players[i].GetComponent<Health>().currentHealth = 1;
                    players[i].Ammo = players[i].AmmoTicks.Length;
                    players[i].GetComponent<Health>().poisoned = false;
                }
            }
            if (players[i].KillCount >= 10)
            {
                GameIsOn = false;
                for (int j = 0; j < players.Length; j++)
                {
                    if (i != j)
                    {
                        players[j].GetComponent<Health>().currentHealth = 0;
                        KillPlayer(j);
                    }
                }
                EndRound();
            }
        }
    }

    private void KillPlayer(int playerIndex)
    {
        Vector3 deathLoc = players[playerIndex].gameObject.transform.position;
        Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        GameObject dEffect = Instantiate(deathEffect, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        DestroyObject(dEffect, 2f);
        players[playerIndex].hurtEffect.Stop();
        if (GameIsOn)
            players[playerIndex].gameObject.transform.position = spawnpoints[UnityEngine.Random.Range(0, spawnpoints.Length)].position;
        else
            players[playerIndex].gameObject.SetActive(false);
    }

    private void EndRound()
    {

    }
}
