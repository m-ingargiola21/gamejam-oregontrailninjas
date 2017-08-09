using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    Image CowboyHealth;
    [SerializeField]
    Image NinjaHealth;

    PlayerCowboy cboy;
    PlayerNinja ninja;

    // Use this for initialization
    void Start () {
        cboy = FindObjectOfType<PlayerCowboy>();
        ninja = FindObjectOfType<PlayerNinja>();
	}
	
	// Update is called once per frame
	void Update () {
        CowboyHealth.fillAmount = cboy.health;
        NinjaHealth.fillAmount = ninja.health;
	}
}
