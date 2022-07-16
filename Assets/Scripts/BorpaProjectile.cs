using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorpaProjectile : Projectile
{

    public Transform GooSpawnerPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Enemy") && !collision.tag.Equals("GooSpawner"))
            Instantiate(GooSpawnerPrefab, this.transform.position, Quaternion.identity);

    }
}
