//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("General")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimebetweenShots = 0.5f;
    [SerializeField] float maxTimebetweenShots = 2f;
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] float bulletSpeed = 8f;
    [SerializeField] GameObject playerLazer;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] float destroyDuation = 1f;

    // Sound effects
    [Header("Sound")]
    [SerializeField] AudioClip enemyDie;
    [SerializeField][Range(0,1)] float enemyDieVolumn = .7f;
    [SerializeField] AudioClip enemyShoot;
    [SerializeField] float enemyShootVolumn = 0.5f;

    // Use this for initialization
    void Start () 
    {
        shotCounter = Random.Range(minTimebetweenShots, maxTimebetweenShots);
	}
	
	// Update is called once per frame
	void Update () 
    {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        // shotCounter needs to takes the time of the time that one frame takes place
        shotCounter -= Time.deltaTime;
        // when the shotCounter goes to 0, fire!
        if (shotCounter <= 0f)
        {
            Fire();
            // Reset the gap between the bullet
            shotCounter = Random.Range(minTimebetweenShots, maxTimebetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            lazerPrefab, transform.position, Quaternion.identity
        )as GameObject;

        // The shooting of the enemy should be downward, so should be negative
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);
        AudioSource.PlayClipAtPoint(enemyShoot, Camera.main.transform.position, enemyShootVolumn);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision here means the object that collide with other objects
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        // This prevent "no instance" error happen.
        if (!damageDealer) { return; }

        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        // shows the destroy animation effect
        var destroyAnimation = Instantiate
        (destroyEffect,
         gameObject.transform.position,
            Quaternion.identity) as GameObject;

        Destroy(destroyAnimation, destroyDuation);

        AudioSource.PlayClipAtPoint(enemyDie, Camera.main.transform.position, enemyDieVolumn);
    }
}
