using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerNinja ninja;
    PlayerCowboy cowboy;
    [SerializeField]
    Transform[] spawnpoints;
    public Rigidbody2D Gravestone;

	// Use this for initialization
	void Start () {
        ninja = FindObjectOfType<PlayerNinja>();
        cowboy = FindObjectOfType<PlayerCowboy>();
	}

    // Update is called once per frame
    void Update() {
        if(ninja != null)
            if (ninja.health <= 0)
            {
                Vector3 deathLoc = ninja.gameObject.transform.position;
                ninja.enabled = false;
                ninja.gameObject.SetActive(false);
                Destroy(ninja.gameObject); 
                Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            }
        if(cowboy != null)
            if (cowboy.health <= 0)
            {
                Vector3 deathLoc = cowboy.gameObject.transform.position;
                cowboy.enabled = false;
                cowboy.gameObject.SetActive(false);
                Destroy(ninja.gameObject);
                Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            }
    }
}
