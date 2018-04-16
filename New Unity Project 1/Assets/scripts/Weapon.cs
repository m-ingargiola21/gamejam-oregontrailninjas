using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D[] Bullets;
    [SerializeField] protected Rigidbody2D Bullet;
    public float speed;               // The speed the rocket will fire at.
    public Transform bulletSpawnLoc;
    bool canFire = true;

    public bool BurstFire;
    public bool CanShootNextBurst;
    public float burstDelay;

    public bool isChargable = false;
    public float reloadTime;
    public bool isCharging;

    private PlayerController playerCtrl;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.

    [SerializeField]
    private float ChargeDamage;
    [SerializeField]
    private float MaxChargeDamage;
    [SerializeField]
    private float additionalDamage;
    [SerializeField]
    private float MaxChargeTime;
    public float timer = 0;


    [SerializeField]
    AudioSource audio;



    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerController>();
        if (playerCtrl.ptype == PlayerType.Cowboy)
        {
            Bullet = Bullets[0];
            isChargable = false;
        }
        else if (playerCtrl.ptype == PlayerType.Spirit)
        {
            Bullet = Bullets[1];
            isChargable = false;
        }
        else if (playerCtrl.ptype == PlayerType.Indian)
        {
            Bullet = Bullets[2];
            isChargable = true;
        }
        else
        {
            Bullet = Bullets[3];
            isChargable = false;
            BurstFire = true;
        }
        
        
    }


    void Update()
    {

        if (!playerCtrl.isReloading)
        // If the fire button is pressed...
        {
            if (Input.GetButtonDown("Reload_P" + (playerCtrl.Identifier +1)))
            {
                playerCtrl.isReloading = true;
                StartCoroutine(playerCtrl.Reload(reloadTime));
            }
            if (!isChargable && !BurstFire)
            {
                if (Input.GetAxis("Fire1_P" + (playerCtrl.Identifier + 1)) < -0.98f && canFire)
                {
          
                    // ... set the animator Shoot trigger parameter and play the audioclip.
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    audio.Play();
                    // If the player is facing right...
                    if (playerCtrl.IsFacingRight)
                        // ... instantiate the rocket facing right and set it's velocity to the right. 
                        SpawnRightFacingBullet();
                    else
                        // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                        SpawnLeftFacingBullet();

                    canFire = false;
                }
            }
            if (isChargable && !BurstFire && canFire)
            {
                if (Input.GetAxis("Fire1_P" + (playerCtrl.Identifier + 1)) < -0.98f)
                {
                    timer += Time.deltaTime;
                    isCharging = true;
                    anim.SetFloat("Charge", (timer / MaxChargeTime));

                }
                if (Input.GetAxis("Fire1_P" + (playerCtrl.Identifier + 1)) > -.1f && isCharging)
                {
                    canFire = false;
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    audio.Play();
                    isCharging = false;
                    // If the player is facing right...
                    if (playerCtrl.IsFacingRight)
                        // ... instantiate the rocket facing right and set it's velocity to the right. 
                        SpawnRightFacingChargeBullet();
                    else
                        // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                        SpawnLeftFacingChargeBullet();
                    timer = 0;
                    ChargeDamage = 0f;
                    anim.SetFloat("Charge", 0);
                }
            }
            if (BurstFire && CanShootNextBurst && canFire)
            {
                if (Input.GetAxis("Fire1_P" + (playerCtrl.Identifier + 1)) < -0.98f)
                {
                    playerCtrl.Ammo--;
                    anim.SetBool("Shooting", true);
                    audio.Play();
                    CanShootNextBurst = false;
                    StartCoroutine(Burst());
                    canFire = false;
                }
            }
            if (Input.GetAxis("Fire1_P" + (playerCtrl.Identifier + 1)) > -.1f)
            {
                canFire = true;
            }
        }
    }

    private void SpawnLeftFacingBullet()
    {
        Rigidbody2D bulletInstance = Instantiate(Bullet, bulletSpawnLoc.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = playerCtrl;
        Vector3 theScale = bulletInstance.transform.localScale;
        theScale.x *= -1;
        bulletInstance.transform.localScale = theScale;
        bulletInstance.velocity = new Vector2(-speed, 0);
        Destroy(bulletInstance.gameObject, 2);
    }

    private void SpawnRightFacingBullet()
    {
        Rigidbody2D bulletInstance = Instantiate(Bullet, bulletSpawnLoc.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = playerCtrl;
        Vector3 theScale = bulletInstance.transform.localScale;
        theScale.x *= -1;
        bulletInstance.transform.localScale = theScale;
        bulletInstance.velocity = new Vector2(speed, 0);
        Destroy(bulletInstance.gameObject, 2);
    }

    private void SpawnRightFacingChargeBullet()
    {
        Rigidbody2D bulletInstance = Instantiate(Bullet, bulletSpawnLoc.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = playerCtrl;
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

    private void SpawnLeftFacingChargeBullet()
    {
        Rigidbody2D bulletInstance = Instantiate(Bullet, bulletSpawnLoc.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
        bulletInstance.gameObject.GetComponent<Projectile>().whoShotMe = playerCtrl;
        if (timer < MaxChargeTime)
            ChargeDamage = (timer / MaxChargeTime * additionalDamage) + bulletInstance.gameObject.GetComponent<Projectile>().Damage;
        else
            ChargeDamage = MaxChargeDamage;
        bulletInstance.gameObject.GetComponent<Projectile>().Damage = ChargeDamage;
        Vector3 theScale = bulletInstance.transform.localScale;
        theScale.x *= -1;
        bulletInstance.transform.localScale = theScale;
        bulletInstance.velocity = new Vector2((-speed * (ChargeDamage / MaxChargeDamage)), 0);
        Destroy(bulletInstance.gameObject, 2);
    }

    IEnumerator Burst()
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerCtrl.IsFacingRight)
                // ... instantiate the rocket facing right and set it's velocity to the right. 
                SpawnRightFacingBullet();
            else
                // Otherwise instantiate the rocket facing left and set it's velocity to the left.
                SpawnLeftFacingBullet();
            yield return new WaitForSeconds(.05f);
        }
        anim.SetBool("Shooting", false);
        StartCoroutine(BurstDelay());
    }

    IEnumerator BurstDelay()
    {
        yield return new WaitForSeconds(burstDelay);
        CanShootNextBurst = true;
    }
}
