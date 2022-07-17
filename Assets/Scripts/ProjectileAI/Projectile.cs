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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
            Destroy(this.gameObject);
    }
}
