using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState { Running, Jumping }

public class PlayerController : MonoBehaviour {

    public MovementState moveState;
    public bool hasJumped = false;
    public bool hasDoubleJumped = false;
    public bool IsFacingRight = true;
    public float MaxSpeed;

    Rigidbody2D rigidbody2D;


	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);

        if (move < 0 && IsFacingRight)
            Flip();
        else if (move > 0 && !IsFacingRight)
            Flip();
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
