﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image Player1Health;
    [SerializeField]
    Image Playe2Health;
    [SerializeField]
    Image[] Player1Marks;
    [SerializeField]
    Image[] Player2Marks;
    [SerializeField]
    Image Playe3Health;
    [SerializeField]
    Image[] Player3Marks;
    [SerializeField]
    Image Playe4Health;
    [SerializeField]
    Image[] Player4Marks;
    PlayerController[] playersTemp;
    public PlayerController[] players;
    GameManager gm;
    // Use this for initialization
    void Start () {
        players = new PlayerController[4];
        playersTemp = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < playersTemp.Length; i++)
        {
            players[playersTemp[i].Identifier - 1] = playersTemp[i];
        }
        gm = GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (players[0] != null)
            Player1Health.fillAmount = players[0].Health;
        if(players[1] != null)
            Playe2Health.fillAmount = players[1].Health;
        if (players[2] != null)
            Playe3Health.fillAmount = players[2].Health;
        if (players[3] != null)
            Playe4Health.fillAmount = players[3].Health;

        for (int i = 0; i < players[0].KillCount && players[0].KillCount <= gm.MaxKills; i++)
        {
            Player1Marks[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < players[1].KillCount && players[1].KillCount <= gm.MaxKills; i++)
        {
            Player2Marks[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < players[2].KillCount && players[2].KillCount <= gm.MaxKills; i++)
        {
            Player3Marks[i].gameObject.SetActive(true);
        }
        //for (int i = 0; i < players[3].KillCount && players[3].KillCount <= gm.MaxKills; i++)
        //{
        //    Player2Marks[i].gameObject.SetActive(true);
        //}

        //for (int i = 0; i < players; i++)
        //{

        //}
    }


}
