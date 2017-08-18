using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour {
    Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(this.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());

        }
    }

}
