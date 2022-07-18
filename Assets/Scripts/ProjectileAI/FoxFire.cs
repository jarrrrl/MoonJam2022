using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FoxFire : Projectile
{
    private AIDestinationSetter aiDestination;
    private void Start()
    {
        if (this.transform.parent == null)
        {
            aiDestination = this.GetComponent<AIDestinationSetter>();
            aiDestination.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HealthController>().ChangeHealth(-1);
            if (collision.transform.parent != null) gameObject.SetActive(false);
            else Destroy(gameObject);
        }
    }

}
