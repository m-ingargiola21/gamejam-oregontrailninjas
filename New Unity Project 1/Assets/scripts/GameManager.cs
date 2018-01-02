using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    Transform[] spawnpoints;
    public Rigidbody2D Gravestone;
    public GameObject deathEffect;
    PlayerController[] playersTemp;
    public PlayerController[] players;
    public int MaxKills = 5;
    public bool GameIsOn;
    public bool GameHasEnded = false;
    public UIManager UI;
    int playersToDeactivate;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsOn = true;
        players = new PlayerController[FindObjectsOfType<PlayerController>().Length];
        playersTemp = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < playersTemp.Length; i++)
        {
            players[playersTemp[i].Identifier] = playersTemp[i];

        }
        if (FindObjectOfType<PlayersToLoad>())
            playersToDeactivate = 4 - FindObjectOfType<PlayersToLoad>().GetPlayerToBeInGame();
        else
            playersToDeactivate = 0;
        for (int i = 0; i < playersToDeactivate; i++)
        {
            players[3-i].gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        UI = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update() {
        
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null && GameIsOn)
            {
                if (players[i].GetComponent<Health>().currentHealth <= 0.001)
                {
                    if (players[i].playerWhoShotMe != null)
                    {
                        players[i].playerWhoShotMe.KillCount++;
                        players[i].playerWhoShotMe.KillList.Add(players[i].ptype);
                    }
                    KillPlayer(i);
                    players[i].playerWhoShotMe = null;
                    players[i].GetComponent<Health>().currentHealth = 1;
                    players[i].Ammo = players[i].AmmoTicks.Length;
                    players[i].GetComponent<Health>().poisoned = false;
                }
            }
            if (players[i].KillCount >= 10 && !GameIsOn && !GameHasEnded)
            {
                for (int j = 0; j < 3-playersToDeactivate; j++)
                {
                    if (i != j)
                    {
                        players[j].GetComponent<Health>().currentHealth = 0;
                        KillPlayer(j);
                    }
                }
                GameHasEnded = true;
                EndRound(i);
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

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].KillCount >= 10)
                GameIsOn = false;
        }

        if (GameIsOn)
            players[playerIndex].gameObject.transform.position = spawnpoints[UnityEngine.Random.Range(0, spawnpoints.Length)].position;
        else
            players[playerIndex].gameObject.SetActive(false);
    }

    private void EndRound(int winner)
    {
        UI.DisplayEndRound(winner);
    }
}
