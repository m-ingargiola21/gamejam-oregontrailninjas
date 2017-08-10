using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    PlayerNinja ninja;
    PlayerCowboy cowboy;
    [SerializeField]
    Transform[] spawnpoints;
    public Rigidbody2D Gravestone;

    public int Cowboykills = 0;
    public int Ninjakills = 0;
    public int MaxKills = 5;
	// Use this for initialization
	void Start () {
        ninja = FindObjectOfType<PlayerNinja>();
        cowboy = FindObjectOfType<PlayerCowboy>();
	}

    // Update is called once per frame
    void Update() {
        if(ninja != null)
            if (ninja.Health <= 0)
            {
                Vector3 deathLoc = ninja.gameObject.transform.position;
                Cowboykills++;
                Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                ninja.gameObject.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
                ninja.Health = 1;
            }
        if(cowboy != null)
            if (cowboy.Health <= 0)
            {
                Vector3 deathLoc = cowboy.gameObject.transform.position;
                Ninjakills++;
                Rigidbody2D grave = Instantiate(Gravestone, deathLoc, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                cowboy.gameObject.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
                cowboy.Health = 1;
            }
        
    }
}
