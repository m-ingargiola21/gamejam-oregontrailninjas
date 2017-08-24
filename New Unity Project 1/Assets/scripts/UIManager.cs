using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    [SerializeField]
    Image[] PlayerHealthImages;
    [SerializeField]
    PlayerUI[] playersUItemp;
    [SerializeField]
    PlayerUI[] playersUI;
    [SerializeField]
    List<Image[]> PlayerMarks;
    PlayerController[] playersTemp;
    public PlayerController[] players;
    GameManager gm;

    void Start () {
        PlayerMarks = new List<Image[]>();
        players = new PlayerController[FindObjectsOfType<PlayerController>().Length];
        playersTemp = FindObjectsOfType<PlayerController>();
        for (int i = 0; i < playersTemp.Length; i++)
        {
            players[playersTemp[i].Identifier - 1] = playersTemp[i];
        }
        int playerNum = 0;
        foreach (PlayerController player in players)
        {
            playerNum++;
        }
        PlayerHealthImages = new Image[4];
        playersUItemp = FindObjectsOfType<PlayerUI>();
        playersUI = new PlayerUI[4];
        for (int i = 0; i < playersUItemp.Length; i++)
        {
            playersUI[playersUItemp[i].Identifier - 1] = playersUItemp[i];
        }
        gm = GetComponent<GameManager>();
        for (int i = 0; i < playersUI.Length; i++)
        {
            PlayerHealthImages[i] = playersUI[i].transform.GetChild(0).GetComponent<Image>();

            if (playersUI[i].Identifier > players.Length)
            {
                playersUI[i].gameObject.SetActive(false);
                playersUI[i].enabled = false;
            }
        }
        for (int i = 0; i < players.Length; i++)
        {    
                PlayerMarks.Add(playersUI[i].transform.GetChild(1).GetComponentsInChildren<Image>());       
        }
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < PlayerMarks[i].Length; j++)
            {
                PlayerMarks[i][j].gameObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < players.Length; i++)
        {
        if (players[i] != null)
            PlayerHealthImages[i].fillAmount = players[i].GetComponent<Health>().currentHealth;
        }

        for (int i = 0; i < players.Length; i++)
        {
            for (int j = 0; j < players[i].KillCount && players[i].KillCount <= gm.MaxKills; j++)
            {
                PlayerMarks[i][j].gameObject.SetActive(true);
            }
        }

    }


}
