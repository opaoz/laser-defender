using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int maxHealth = 32;
    [SerializeField] int health;

    [Header("Projectile")]
    [SerializeField] GameObject shot;
    [SerializeField] float projectileSpeed = 11f;
    [SerializeField] float projectileFireSpeed = 0.1f;

    [Header("Sounds")]
    [Range(0, 100)] [SerializeField] float volume = 0.3f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shotSound;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Level level;
    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        level = FindObjectOfType<Level>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        Vector3 zeroPosition = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        xMin = zeroPosition.x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = zeroPosition.y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    void Move()
    {
        float delta = Time.deltaTime * moveSpeed;
        float deltaX = Input.GetAxis("Horizontal") * delta;
        float deltaY = Input.GetAxis("Vertical") * delta;

        float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        bool sound = true;

        while (true)
        {
            GameObject laser = Instantiate(shot, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            if (sound)
            {
                AudioSource.PlayClipAtPoint(shotSound, transform.position, volume);
            }

            sound = !sound;

            yield return new WaitForSeconds(projectileFireSpeed);
        }
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
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, volume);

        Destroy(gameObject);
        level.LoadGameOver();
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
