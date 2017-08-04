using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState { Running, Jumping }

public class PlayerController : MonoBehaviour {

    public MovementState moveState;
    public bool IsFacingRight = true;
    public float MaxSpeed;
    public float JumpForce = 700;
    public bool doubleJump = false;
    Rigidbody2D rigidbody2D;

    Animator anim;
    bool grounded = false;
    [SerializeField]
    Transform groundCheck;
    float groundedRadius = 0.2f;
    public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        if (grounded)
            doubleJump = false;
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);

        if (move < 0 && IsFacingRight)
            Flip();
        else if (move > 0 && !IsFacingRight)
            Flip();
    }

    void Update()
    {
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, JumpForce));
            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
