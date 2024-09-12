using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.GetComponent<Monster>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(transform.position.y > 10 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
