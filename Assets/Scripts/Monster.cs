using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int maxHp = 50;
    private int hp = 50;
    public int damage = 10;
    public Slider HpSlider;
    private float lerpedHp;
    private float lerpSpeed = 8f;
    public float AttackDelay;


    private void Start()
    {
        hp = maxHp;
        lerpedHp = hp;
    }
    private void Update()
    {
        if (transform.position.y < -8f)
        {
            Disappear();
        }

        lerpedHp = Mathf.Lerp(lerpedHp, hp, lerpSpeed * Time.deltaTime);
        HpSlider.value = lerpedHp / maxHp;

        AttackDelay -= Time.deltaTime;
    }

    public void PerformAttack()
    {
        if(AttackDelay <= 0)
        {
            if (Game.Battle.player != null)
            {
                Game.Battle.player.TakeDamage(damage);
            }
            AttackDelay = 0.5f;
        }
    }

    public void TakeDamage(int damage)
    {
        HpSlider.gameObject.SetActive(true);
        hp -= damage;
        HpSlider.value = lerpedHp / maxHp;
        if (hp <= 0)
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