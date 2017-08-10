using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player") && this.GetComponent<Rigidbody2D>().velocity.magnitude > 2)
        {
            if (collision.gameObject.GetComponent<PlayerController>().Health > .2f)
            {
                collision.gameObject.GetComponent<PlayerController>().Health -= .1f;
            }

        }
    }

}
