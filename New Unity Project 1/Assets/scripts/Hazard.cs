using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
    [SerializeField]
    Rigidbody2D hazard;
    public float speed;
    public float timer;

    
	// Use this for initialization
	void Start () {
        timer = Random.Range(5, 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (timer <= 0)
        {
            SpawnHazard();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void SpawnHazard()
    {
        Rigidbody2D bulletInstance = Instantiate(hazard, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        if (this.gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            Vector3 theScale = bulletInstance.transform.localScale;
            theScale.x *= -1;
            bulletInstance.transform.localScale = theScale;
            bulletInstance.velocity = new Vector2(speed, 0);
        }
        else
            bulletInstance.velocity = new Vector2(-speed, 0);
        Destroy(bulletInstance.gameObject, 5);
        timer = Random.Range(5, 10);
    }
}
