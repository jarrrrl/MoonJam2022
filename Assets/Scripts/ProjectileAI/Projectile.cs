using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
   public void OnShoot(float rotation)
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        float speed = 5f;
        rb.AddForce(new Vector2(rotation, 0) * speed, ForceMode2D.Impulse);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<HealthController>().ChangeHealth(-1);
        else if (collision.CompareTag("Ground")) Destroy(gameObject);
    }
}
