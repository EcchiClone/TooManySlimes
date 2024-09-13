using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedObject : MonoBehaviour
{
    private float initSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private float currentSpeed;
    private bool moveEnd = false;
    public bool canGet = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 초기 속도
        initSpeed = Random.Range(5f, 10f);
        currentSpeed = initSpeed;

        // 북쪽 40도 범위
        float randomAngle = Random.Range(-20f, 20f);
        moveDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

        rb.velocity = moveDirection * currentSpeed;
    }

    void Update()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 4f);

        rb.velocity = moveDirection * currentSpeed;

        if (currentSpeed < 0.01f && !moveEnd)
        {
            moveEnd = true;
            canGet = true;
            rb.velocity = Vector2.zero;
            Game.Battle.entities.Add(gameObject);
        }
    }
}
