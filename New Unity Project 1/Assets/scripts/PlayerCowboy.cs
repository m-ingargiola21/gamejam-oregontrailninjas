using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCowboy : MonoBehaviour
{
    public bool IsFacingRight = true;
    public float MaxSpeed;
    public float JumpForce = 100;
    public bool doubleJump = false;
    Rigidbody2D rigidbody2D;
    public int Identifier;

    public float health = 1;

    Animator anim;
    public bool grounded = false;
    [SerializeField]
    Transform groundCheck;
    float groundedRadius = 0.05f;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (grounded)
            doubleJump = false;


        float move = Input.GetAxis("Horizontal" + Identifier);
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);

        if (move < 0 && IsFacingRight)
            Flip();
        else if (move > 0 && !IsFacingRight)
            Flip();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        if (rigidbody2D.velocity.y > 6f)
            rigidbody2D.AddForce(new Vector2(0, -JumpForce));

        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump" + Identifier))
        {
            anim.SetBool("Ground", false);
            if (rigidbody2D.velocity.y < (-MaxSpeed / 2))
            {
                rigidbody2D.AddForce(new Vector2(0, 2 * JumpForce));
                Debug.Log("I'm Falling TOO FAST");
            }
            else
                rigidbody2D.AddForce(new Vector2(0, JumpForce));


            if (!doubleJump && !grounded)
            {
                doubleJump = true;
                //Debug.Log("i shouldn't be able to jump");
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

