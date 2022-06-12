using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    public Joystick joystick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] int selfDamage = 50;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 2f;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume = 0.25f;
    Coroutine firingCoroutine;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    float xMin;
    float yMin;
    float xMax;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
    }

    public void StartFire()
    {
        firingCoroutine = StartCoroutine(FireContnuously());
    }

    public void StopFire()
    {
        StopCoroutine(firingCoroutine);
    }

    IEnumerator FireContnuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {
        horizontalMove =joystick.Horizontal * moveSpeed * Time.deltaTime;
        verticalMove =joystick.Vertical * moveSpeed * Time.deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + horizontalMove, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + verticalMove, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
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

    public void GetDamageOfWrongWord()
    {
        health -= selfDamage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    public int GetHealth()
    {
        return health;
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
