using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public bool destroyWithTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.GetComponent<Monster>().TakeDamage(damage);
            if(destroyWithTouch)
                Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (transform.position.y > 15 || transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }
}
