using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 2f;
    Coroutine firingCoroutine;
    bool isPressed = false;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    float xMin;
    float yMin;
    float xMax;
    float yMax;

    // Start is called before the first frame update
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

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
