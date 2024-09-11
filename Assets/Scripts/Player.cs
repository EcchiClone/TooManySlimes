using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private HashSet<Collider2D> enemieCollisions = new HashSet<Collider2D>();

    public float maxHealth = 1000f;
    public float health = 1000f;
    public float attackPower = 20f;
    public bool isInCombat = false;
    public Slider HpSlider;

    private float playerColliderY;

    private void Start()
    {
        playerColliderY = GetComponent<CapsuleCollider2D>().size.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            float collisionColY = collision.transform.GetComponent<BoxCollider2D>().size.y;
            float collisionPosY = collision.transform.position.y;
            float delta = (playerColliderY + collisionColY) / 2 - (collisionPosY - transform.position.y);
            if (delta < 0.1)
            {
                enemieCollisions.Add(collision);

                Game.Battle.gameState = GameState.Stop;
                Debug.Log("Enemy Entered, GameState: Stop");
            }
        }
        if (collision.transform.CompareTag("EventLine"))
        {
            if (transform.position.x < 0)
            {
                Debug.Log("왼쪽 선택");
            }
            else
            {
                Debug.Log("오른쪽 선택");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.transform.CompareTag("Enemy"))
        {
            if (!enemieCollisions.Contains(collision))
            {
                enemieCollisions.Add(collision);
            }
            if (collision.transform.GetComponent<Monster>().AttackDelay <= 0)
            {
                collision.transform.GetComponent<Monster>().PerformAttack();
                collision.transform.GetComponent<Monster>().TakeDamage(attackPower);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            enemieCollisions.Remove(collision);

            if (enemieCollisions.Count == 0)
            {
                Game.Battle.gameState = GameState.Move;  // 더 이상 적과 충돌이 없으면 Move 상태로 변경
                Debug.Log("All Enemies Exited, GameState: Move");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HpSlider.gameObject.SetActive(true);
        health -= damage;
        HpSlider.value = health / maxHealth;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isInCombat = false;
        Destroy(gameObject);
    }


}