using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float maxHealth = 50f;
    private float health = 50f;
    public float attackPower = 10f;
    public Slider HpSlider;
    private float lerpedHealth;
    private float lerpSpeed = 8f;
    public float AttackDelay;

    private void Awake()
    {
    }
    private void Start()
    {
        health = maxHealth;
        lerpedHealth = health;
    }
    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Disappear();
        }

        lerpedHealth = Mathf.Lerp(lerpedHealth, health, lerpSpeed * Time.deltaTime);
        HpSlider.value = lerpedHealth / maxHealth;

        AttackDelay -= Time.deltaTime;
    }

    public void PerformAttack()
    {
        if(AttackDelay <= 0)
        {
            if (Game.Battle.player != null)
            {
                Game.Battle.player.TakeDamage(attackPower);
            }
            AttackDelay = 0.5f;
        }
    }

    public void TakeDamage(float damage)
    {
        HpSlider.gameObject.SetActive(true);
        health -= damage;
        HpSlider.value = lerpedHealth / maxHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Game.Battle.monsters.Remove(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }

    public void Disappear()
    {
        Game.Battle.monsters.Remove(this);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }
}