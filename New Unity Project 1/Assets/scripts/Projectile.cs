using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public PlayerController whoShotMe;
    public int GraveSpeed;
    public bool isPoisonous;
    public float Damage;


    void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < 4; i++)
        {
            if(!isPoisonous)
                if (col.gameObject.tag == ("Player" + i.ToString()))
                {
                    col.gameObject.GetComponent<PlayerController>().Health -= Damage;
                }
            if (isPoisonous)
                if (col.gameObject.tag == ("Player" + i.ToString()))
                {
                    col.gameObject.GetComponent<PlayerController>().poisoned = true;
                    col.gameObject.GetComponent<PlayerController>().ResetPoison();
                    col.gameObject.GetComponent<PlayerController>().Health -= Damage;
                }
        }
        if (col.gameObject.tag == "GraveStone")
        {
            col.GetComponent<Rigidbody2D>().AddForce(this.gameObject.GetComponent<Rigidbody2D>().velocity * GraveSpeed);
            Destroy(this.gameObject);
        }
    }
    
}
