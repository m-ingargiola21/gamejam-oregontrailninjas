using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ninja")
        {
            FindObjectOfType<PlayerNinja>().Health -= .10f;
        }
        else if (col.gameObject.tag == "Cowboy")
        {
            FindObjectOfType<PlayerCowboy>().Health -= .10f;
        }
        Destroy(this.gameObject);
    }
}
