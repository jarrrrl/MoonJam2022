using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorpaProjectile : Projectile
{

    public Transform GooSpawnerPrefab;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Ground"))
        {
            Instantiate(GooSpawnerPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
