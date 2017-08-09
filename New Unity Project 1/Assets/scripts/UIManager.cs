using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image CowboyHealth;
    [SerializeField]
    Image NinjaHealth;
    [SerializeField]
    Image[] cowboymarks;
    [SerializeField]
    Image[] ninjamarks;
    PlayerCowboy cboy;
    PlayerNinja ninja;
    GameManager gm;
    // Use this for initialization
    void Start () {
        cboy = FindObjectOfType<PlayerCowboy>();
        ninja = FindObjectOfType<PlayerNinja>();
        gm = GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (cboy != null)
        CowboyHealth.fillAmount = cboy.Health;
        if(ninja != null)
        NinjaHealth.fillAmount = ninja.Health;

        for (int i = 0; i < gm.Cowboykills && gm.Cowboykills < 10; i++)
        {
            cowboymarks[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < gm.Ninjakills && gm.Cowboykills < 10; i++)
        {
            ninjamarks[i].gameObject.SetActive(true);
        }
    }


}
