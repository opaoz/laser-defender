using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int health = 64;
    [SerializeField] bool hasGun = true;
    [SerializeField] int givenScore = 5;

    [Header("Projectile")]
    [SerializeField] GameObject shotPrefab;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float shotCounter;
    [SerializeField] float projectileSpeed = 9f;

    [Header("Death particles")]
    [SerializeField] GameObject particlesPrefab;

    [Header("Sounds")]
    [Range(0, 100)] [SerializeField] float volume = 0.3f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shotSound;

    Status status;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        status = FindObjectOfType<Status>();
    }

    private void Update()
    {
        if (hasGun)
        {
            CountDownAndShoot();
        }
    }

    void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            Fire();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(shotPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shotSound, transform.position, volume);
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            ProcessHit(damageable);
        }

    }

    void ProcessHit(Damageable damageable)
    {
        health -= damageable.GetDamage();

        if (health <= 0)
        {
            Die();
        }

        damageable.Hit();
    }

    void Die()
    {
        var particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);

        status.AddScore(givenScore);

        Destroy(particles, 1);
        Destroy(gameObject);
    }
}
