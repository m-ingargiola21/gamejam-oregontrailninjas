using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public PlayerController whoShotMe;
    public bool isPoisonous;
    public float Damage;


    void OnTriggerEnter2D(Collider2D col)
    {
            if(!isPoisonous)
                if (col.gameObject.tag == ("Player"))
                {
                    col.gameObject.GetComponent<Health>().TakeDamage(Damage);
                }
            if (isPoisonous)
                if (col.gameObject.tag == ("Player"))
                {
                    col.gameObject.GetComponent<Health>().poisoned = true;
                    col.gameObject.GetComponent<Health>().ResetPoison();
                    col.gameObject.GetComponent<Health>().TakeDamage(Damage);
                }    
    }
    
}
