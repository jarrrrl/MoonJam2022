using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorpaProjectile : Projectile
{

    public Transform GooSpawnerPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            return; //damage here
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(GooSpawnerPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Enemy") && !collision.CompareTag("Projectile")) Destroy(gameObject);

    }
}
