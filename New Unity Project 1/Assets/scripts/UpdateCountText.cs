using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCountText : MonoBehaviour {
    Text playerCountText;
    public int Players;
	// Use this for initialization
	void Start () {
        playerCountText = GetComponent<Text>();
        Players = 2;
	}
	
	// Update is called once per frame
	void Update () {
        playerCountText.text = "Players: " + Players.ToString();
        int i = 1;
        while (i < 5)
        {
            if (Input.GetButtonDown("Reload_P" + i.ToString()))
            {
                Players++;
                if (Players > 4)
                    Players = 2;
            }
            i++;
        }
    }
}
