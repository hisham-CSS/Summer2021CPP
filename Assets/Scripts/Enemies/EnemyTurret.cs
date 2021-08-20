using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public Projectile projectilePrefab;

    public float projectileForce;

    public float projectileFireRate;

    float timeSinceLastFire = 0.0f;
    public int health;

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2.0f;
        }

        if (health <= 0)
        {
            health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!anim.GetBool("Fire"))
        {
            //HINT: CHECK SOMETHING PRIOR TO FIRING TO DETERMINE WHICH DIRECTION TO FIRE - MAYBE ALSO INCLUDE DISTANCE
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetBool("Fire", true);
            }
        }
    }

    public void Fire()
    {
        //HINT 2: IF YOU KNOW THE DIRECTION YOU ARE FACING - MAYBE OUR FIRE LOGIC CAN BE SIMILAR TO THE PLAYER
        timeSinceLastFire = Time.time;
        Projectile temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        temp.speed = projectileForce;
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Destroy(collision.gameObject);

            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
