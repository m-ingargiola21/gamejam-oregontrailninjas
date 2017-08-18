using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D Bullet;
    public float speed;               // The speed the rocket will fire at.

    public bool BurstFire;
    public bool CanShootNextBurst;
    public float burstDelay;

    public bool isChargable = false;
    public float reloadTime;
    public bool isCharging;

    private PlayerController playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.
    //[SerializeField]
    public float ChargeDamage;
    //[SerializeField]
    public float MaxChargeDamage;
    //[SerializeField]
    public float additionalDamage;
    //[SerializeField]
    public float MaxChargeTime;
    public float timer = 0;
    void Awake()
    {
        // Setting up the references.
        anim = transform.parent.gameObject.GetComponent<Animator>();
        playerCtrl = transform.parent.GetComponent<PlayerController>();
    }


    void Update()
    {
        if (!playerCtrl.isReloading)
        // If the fire button is pressed...
        {
            if (Input.GetButtonDown("Reload_P" + playerCtrl.Identifier.ToString()))
            {
                playerCtrl.isReloading = true;
                StartCoroutine(playerCtrl.Reload(reloadTime));
            }
            if (!isChargable && !BurstFire)
            {
                if (Input.GetButtonDown("Fire1_P" + playerCtrl.Identifier.ToString()))
                {
                    // ... set the animator Shoot trigger parameter and play the audioclip.
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    //audio.Play();
                    
                    // If the player is facing right...
                    if (playerCtrl.IsFacingRight)
                    {
                        // ... instantiate the rocket facing right and set it's velocity to the right. 
                        Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                        Vector3 theScale = bulletInstance.transform.localScale;
                        theScale.x *= -1;
                        bulletInstance.transform.localScale = theScale;
                        bulletInstance.velocity = new Vector2(speed, 0);
                        Destroy(bulletInstance.gameObject, 2);
                    }
                    else
                    {
                        // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                        Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                        Vector3 theScale = bulletInstance.transform.localScale;
                        theScale.x *= -1;
                        bulletInstance.transform.localScale = theScale;
                        bulletInstance.velocity = new Vector2(-speed, 0);
                        Destroy(bulletInstance.gameObject, 2);
                    }
                }
            }
            if(isChargable && !BurstFire)
            {
                if (Input.GetButton("Fire1_P" + playerCtrl.Identifier.ToString()))
                {
                    timer += Time.deltaTime;
                    isCharging = true;
                    anim.SetFloat("Charge",(timer/MaxChargeTime));

                }
                if (Input.GetButtonUp("Fire1_P" + playerCtrl.Identifier.ToString()))
                {
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    //audio.Play();
                    isCharging = false;
                    // If the player is facing right...
                    if (playerCtrl.IsFacingRight)
                    {
                        // ... instantiate the rocket facing right and set it's velocity to the right. 
                        Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                        if (timer < MaxChargeTime)
                            ChargeDamage = (timer / MaxChargeTime * additionalDamage) + bulletInstance.gameObject.GetComponent<Projectile>().Damage;
                        else
                            ChargeDamage = MaxChargeDamage;
                        bulletInstance.gameObject.GetComponent<Projectile>().Damage = ChargeDamage;
                        Vector3 theScale = bulletInstance.transform.localScale;
                        theScale.x *= -1;
                        bulletInstance.transform.localScale = theScale;
                        bulletInstance.velocity = new Vector2((speed * (ChargeDamage / MaxChargeDamage)), 0);
                        Destroy(bulletInstance.gameObject, 2);
                    }
                    else
                    {
                        // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                        Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                        if (timer < MaxChargeTime)
                            ChargeDamage = (timer / MaxChargeTime * additionalDamage) + bulletInstance.gameObject.GetComponent<Projectile>().Damage;
                        else
                            ChargeDamage = MaxChargeDamage;
                        bulletInstance.gameObject.GetComponent<Projectile>().Damage = ChargeDamage;
                        Vector3 theScale = bulletInstance.transform.localScale;
                        theScale.x *= -1;
                        bulletInstance.transform.localScale = theScale;
                        bulletInstance.velocity = new Vector2((-speed*(ChargeDamage/MaxChargeDamage)), 0);
                        Destroy(bulletInstance.gameObject, 2);
                    }
                    timer = 0;
                    anim.SetFloat("Charge", 0);
                }
            }
            if (BurstFire && CanShootNextBurst)
            {
                if(Input.GetButtonDown("Fire1_P" + playerCtrl.Identifier.ToString()))
                {
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    CanShootNextBurst = false;
                    StartCoroutine(Burst());
                }
            }
        }
    }


    IEnumerator Burst()
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerCtrl.IsFacingRight)
            {
                // ... instantiate the rocket facing right and set it's velocity to the right. 
                Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                Vector3 theScale = bulletInstance.transform.localScale;
                theScale.x *= -1;
                bulletInstance.transform.localScale = theScale;
                bulletInstance.velocity = new Vector2(speed, 0);
                Destroy(bulletInstance.gameObject, 2);
            }
            else
            {
                // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                Rigidbody2D bulletInstance = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = transform.parent.gameObject.GetComponent<PlayerController>();
                Vector3 theScale = bulletInstance.transform.localScale;
                theScale.x *= -1;
                bulletInstance.transform.localScale = theScale;
                bulletInstance.velocity = new Vector2(-speed, 0);
                Destroy(bulletInstance.gameObject, 2);
            }
            yield return new WaitForSeconds(.05f);
        }
        StartCoroutine(BurstDelay());
    }

    IEnumerator BurstDelay()
    {
        yield return new WaitForSeconds(burstDelay);
        CanShootNextBurst = true;
    }
}
