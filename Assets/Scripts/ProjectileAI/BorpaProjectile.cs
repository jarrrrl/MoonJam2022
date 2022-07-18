using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorpaProjectile : Projectile
{

    public Transform GooSpawnerPrefab;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            SpawnGoo();
        }
        else if(collision.collider.CompareTag("Player"))
            collision.collider.GetComponent<HealthController>().ChangeHealth(-1);

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<HealthController>().ChangeHealth(-1);
        else if (collision.CompareTag("GooSpawner")) Destroy(gameObject);

    }
    private void SpawnGoo()
    {
        Instantiate(GooSpawnerPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
