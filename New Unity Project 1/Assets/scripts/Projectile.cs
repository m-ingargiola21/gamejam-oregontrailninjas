using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public PlayerController whoShotMe;
    public int GraveSpeed;
    void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < 4; i++)
        {
            if (col.gameObject.tag == ("Player" + i.ToString()))
            {
                if (col.gameObject.GetComponent<PlayerController>().Health - .1f <= 0)
                    whoShotMe.KillCount++;
                col.gameObject.GetComponent<PlayerController>().Health -= .1f;
                Destroy(this.gameObject);
            }
        }
        if (col.gameObject.tag == "GraveStone")
        {
            col.GetComponent<Rigidbody2D>().AddForce(this.gameObject.GetComponent<Rigidbody2D>().velocity * GraveSpeed);
            Destroy(this.gameObject);
        }
    }
}
