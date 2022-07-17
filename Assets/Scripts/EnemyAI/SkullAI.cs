using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkullAI : EnemyAI
{

    private AIDestinationSetter aiDestination;

    // Start is called before the first frame update
    void Start()
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
