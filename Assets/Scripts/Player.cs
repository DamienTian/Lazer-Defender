using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configs
    // Header creates a title for all the SerializeField
    [Header("Player")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float paddingX = 0.05f;
    [SerializeField] float paddingY = 0.03f;
    [SerializeField] public int health = 500;
    [SerializeField] AudioClip playerDie;
    [SerializeField] float playerDieVolumn = 0.7f;
    [SerializeField] AudioClip playerShoot;
    [SerializeField] float playerShootVolumn = 0.7f;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] float destroyDuation = 1f;

    [Header("Lazer")]
    [SerializeField] GameObject lazerPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletFiringPeriod = 0.2f;

    Coroutine firingCoroutine;
    //LifeDisplay lifeDisplay;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Use this for initialization
    void Start()
    {
        // Setup the boundaries
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamara = Camera.main;

        // Use the game camera to setup the boundaries of how ship move
        xMin = gameCamara.ViewportToWorldPoint(new Vector2(0, 0)).x + paddingX;
        xMax = gameCamara.ViewportToWorldPoint(new Vector2(1, 0)).x - paddingX;
        yMin = gameCamara.ViewportToWorldPoint(new Vector2(0, 0)).y + paddingY;
        yMax = gameCamara.ViewportToWorldPoint(new Vector2(0, 1)).y - paddingY;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    // Handling hit
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
        FindObjectOfType<LifeDisplay>().LoseLife();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDie, Camera.main.transform.position, playerDieVolumn);

        // shows the destroy animation effect
        var destroyAnimation = Instantiate
        (destroyEffect,
         gameObject.transform.position,
            Quaternion.identity) as GameObject;

        Destroy(destroyAnimation, destroyDuation);
    }

    private void Move()
    {
        /*
        Refer to the Unity section:
        Edit -> Project Setting -> Input

        > Using "Horizontal" as the sign of assigning the keyboard movements to the object.
        In this case, it is keyboard controlling.
        
        > Time.deltaTime: makes the frame rate independent fromt the computer's calculation speed
          This gives the same game experience to slow and fast computers
        */

        // P.S. both deltaX and newXPos are float
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Review: Mathf.Clamp sets up the boundaries of the ship moves
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);

    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            /* 
             * StartCoroutine: 
               Iterating the a IEnumerator method
             */
            /* This example give the player to shooting bullets continuously while
             the button was pressed down. */
            firingCoroutine = StartCoroutine(FireContinuously());
            //Debug.Log(lazerPrefab.name);
        }

        // Stop shooting
        if (Input.GetButtonUp("Fire1"))
        {
            // A brutal way: stop all coroutines
            // StopAllCoroutines();

            // Instead:
            StopCoroutine(firingCoroutine);
        }
    }

    // IEnumerator method is a self-iterating method
    IEnumerator FireContinuously()
    {
        while (true)
        {
            // Quaternion: setup the object rotation
            //             Quaternion.identity means no rotation
            GameObject laser = Instantiate
                (lazerPrefab,
                transform.position,
                Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            AudioSource.PlayClipAtPoint(playerShoot, Camera.main.transform.position, playerShootVolumn);
            yield return new WaitForSeconds(bulletFiringPeriod);
        }
    }
}