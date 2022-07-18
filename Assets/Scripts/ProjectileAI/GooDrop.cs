using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooDrop : Projectile
{

    public GameObject GooDropFFXPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<HealthController>().ChangeHealth(-1);
        else if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Ground")) Destroy(gameObject);
        GameObject GooFFX = Instantiate(GooDropFFXPrefab, this.transform.position, Quaternion.identity);
        Destroy(GooFFX, 1f);
    }
}
