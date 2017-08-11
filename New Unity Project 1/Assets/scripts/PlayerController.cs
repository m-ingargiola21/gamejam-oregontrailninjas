using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public bool IsFacingRight = true;
    public float MaxSpeed;
    public float JumpForce = 100;
    public bool doubleJump = false;
    public Rigidbody2D rigidbody2D;
    public int Identifier;

    public float Health = 1;
    public int KillCount;

    public bool poisoned;
    float poisonTimer = 2f;

    Animator anim;
    public bool grounded = false;
    [SerializeField]
    Transform groundCheck;
    float groundedRadius = 0.05f;
    public LayerMask whatIsGround;

    [SerializeField]
    public SpriteRenderer[] AmmoTicks;
    public int Ammo;
    [SerializeField]
    Sprite unusedAmmo;
    [SerializeField]
    Sprite usedAmmo;
    public bool isReloading;

    public PlayerController playerWhoShotMe;

    

    private void Awake()
    {
        Health = 1;
    }
    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Ammo = AmmoTicks.Length;
    }

    void FixedUpdate()
    {
        if (grounded)
            doubleJump = false;

        if (Input.GetAxis("Horizontal" + Identifier) > 0)
            anim.SetBool("Shooting", false);
        float move = Input.GetAxis("Horizontal" + Identifier);
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);

        if (move < 0 && IsFacingRight)
            Flip();
        else if (move > 0 && !IsFacingRight)
            Flip();

        if (poisoned)
            Health -=  Time.deltaTime/8;
    }

    void Update()
    {
        if (poisoned)
        {
            if (poisonTimer > 0)
                poisonTimer -= Time.deltaTime;
            else
            {
                poisoned = !poisoned;
                ResetPoison();
            }
        }


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

        if (Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround))
            Debug.Log(gameObject.name + ": " + Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround).name);

        anim.SetBool("Ground", grounded);

        if (rigidbody2D.velocity.y > 8f)
            rigidbody2D.AddForce(new Vector2(0, -JumpForce));

        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump" + Identifier))
        {
            anim.SetBool("Ground", false);
            if (rigidbody2D.velocity.y < (-MaxSpeed / 2))
            {
                rigidbody2D.AddForce(new Vector2(0, 2 * JumpForce));
            }
            else
                rigidbody2D.AddForce(new Vector2(0, JumpForce));


            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }

        UpdateAmmoUI();
        CheckForReload();

    }

    private void UpdateAmmoUI()
    {
        for (int i = 0; i < Ammo; i++)
        {
            AmmoTicks[i].sprite = unusedAmmo;
        }
        for (int i = AmmoTicks.Length; i > Ammo; i--)
        {
            AmmoTicks[i-1].sprite = usedAmmo;
        }
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void CheckForReload()
    {
        if (Ammo == 0 && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(2.0f);
        isReloading = false;
        Ammo = AmmoTicks.Length;
    }

    public void ResetPoison()
    {
        poisonTimer = 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            playerWhoShotMe = collision.GetComponent<Projectile>().whoShotMe;
            Destroy(collision.gameObject);
        }
    }


}