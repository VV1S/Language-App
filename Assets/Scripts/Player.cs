using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 1f;
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

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontalMove =joystick.Horizontal * moveSpeed * Time.deltaTime;
        verticalMove =joystick.Vertical * moveSpeed * Time.deltaTime;

        var newXPos = Mathf.Clamp(transform.position.x + horizontalMove, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + verticalMove, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
}
