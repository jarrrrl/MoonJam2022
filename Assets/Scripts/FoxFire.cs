using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FoxFire : Projectile
{
    private AIDestinationSetter aiDestination;
    private void Start()
    {
        aiDestination = this.GetComponent<AIDestinationSetter>();
        aiDestination.target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            Destroy(this.gameObject);
    }

}
