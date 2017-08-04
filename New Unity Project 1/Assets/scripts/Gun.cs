using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody2D Bullet;              // Prefab of the rocket.
    public float speed = 20f;               // The speed the rocket will fire at.
    


    private PlayerCowboy playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.


    void Awake()
    {
        // Setting up the references.
        anim = transform.parent.gameObject.GetComponent<Animator>();
        playerCtrl = transform.parent.GetComponent<PlayerCowboy>();
    }


    void Update()
    {
        // If the fire button is pressed...
        if (Input.GetButtonDown("Fire1_P1"))
        {
            // ... set the animator Shoot trigger parameter and play the audioclip.
            
            //audio.Play();

            // If the player is facing right...
            if (playerCtrl.IsFacingRight)
            {
                // ... instantiate the rocket facing right and set it's velocity to the right. 
                Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(speed, 0);
                Destroy(bulletInstance.gameObject, 2);
            }
            else
            {
                // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-speed, 0);
                Destroy(bulletInstance.gameObject, 2);
            }
        }
    }
}