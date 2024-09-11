using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    private float moveSpeed = 50f;

    private Vector2 touchStartPos;
    private Vector2 playerStartPos;
    private bool isDragging = false;

    void Update()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 드래그 시작 위치 및 그 시점의 플레이어 시작 위치 보존
            touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerStartPos = playerRigidbody.position;
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // result = 드래그 중 위치 - 드래그 시작 위치
            Vector2 currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 result = currentTouchPos - touchStartPos;

            // 플레이어 시작 위치.x + result.x 로 유도
            float clampedX = Mathf.Clamp(playerStartPos.x + result.x, -1.8f, 1.8f);
            Vector2 targetPosition = new Vector2(clampedX, playerRigidbody.position.y);
            LerpPlayer(targetPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
    void LerpPlayer(Vector2 targetPosition)
    {
        Vector2 newPosition = Vector2.Lerp(playerRigidbody.position, targetPosition, moveSpeed * Time.deltaTime);
        playerRigidbody.MovePosition(newPosition);
    }

}
