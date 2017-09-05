using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PlayerType ptype;

#region Move Variables
    public bool IsFacingRight = true;
    public float MaxSpeed;
    public float JumpForce = 100;
    public bool doubleJump = false;
    public Rigidbody2D rigidbody2D;
    public int Identifier;
    public bool grounded = false;
    [SerializeField]
    Transform groundCheck;
    float groundedRadius = 0.05f;
    public LayerMask whatIsGround;
#endregion

    public int KillCount;
    Animator anim;

#region Ammo and Reloading
    [SerializeField]
    public SpriteRenderer[] AmmoTicks;
    public int Ammo;
    [SerializeField]
    Sprite unusedAmmo;
    [SerializeField]
    Sprite usedAmmo;
    public bool isReloading;
    public float reloadTime;
#endregion

    public PlayerController playerWhoShotMe;
    protected Weapon myWeapon;
    public ParticleSystem hurtEffect;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Ammo = AmmoTicks.Length;
        myWeapon = GetComponentInChildren<Weapon>();
        hurtEffect = transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (grounded)
            doubleJump = false;
        
        if (Input.GetAxis("Horizontal" + Identifier) > 0)
                anim.SetBool("Shooting", false);
        float move = Input.GetAxis("Horizontal" + Identifier);

        if (!myWeapon.isCharging)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
            rigidbody2D.velocity = new Vector2(move * MaxSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            anim.SetFloat("Speed", 0);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }
        if (move < 0 && IsFacingRight)
            Flip();
        else if (move > 0 && !IsFacingRight)
            Flip();
        //8 is the total seconds that the player will be poisoned for divided by the percent health that each poison dart takes away (1 seconds / .25(25%))
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        if (Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround))
            //Debug.Log(gameObject.name + ": " + Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround).name);

        anim.SetBool("Ground", grounded);

        if (rigidbody2D.velocity.y > 8f)
            rigidbody2D.AddForce(new Vector2(0, -JumpForce));
        if(!myWeapon.isCharging)
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
            StartCoroutine(Reload(reloadTime));
        }
    }

    public IEnumerator Reload(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        Ammo = AmmoTicks.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            playerWhoShotMe = collision.GetComponent<Projectile>().whoShotMe;
            Destroy(collision.gameObject);
            if (playerWhoShotMe.transform.position.x - transform.position.x > 0)
                hurtEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            else
                hurtEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                
            hurtEffect.Play();
        }
    }
}