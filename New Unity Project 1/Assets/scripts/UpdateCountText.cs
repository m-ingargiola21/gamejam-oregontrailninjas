using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCountText : MonoBehaviour {
    Text playerCountText;
    int players;
	// Use this for initialization
	void Start () {
        playerCountText = GetComponent<Text>();
        players = 2;
	}
	
	// Update is called once per frame
	void Update () {
        playerCountText.text = "Player Count: " + players.ToString();
        int i = 1;
        while (i < 5)
        {
            if (Input.GetButtonDown("Reload_P" + i.ToString()))
            {
                players++;
                if (players > 4)
                    players = 2;
            }
            i++;
        }
    }
}
