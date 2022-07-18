using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkullAI : EnemyAI
{

    private AIDestinationSetter aiDestination;
    private AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        aiDestination = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        aiDestination.target = playerTransform;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            aiPath.canMove = true;
        }
        else
        {
            aiPath.canMove = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) collision.GetComponent<HealthController>().ChangeHealth(-1);
    }
    protected override void OnDestroy()
    {
        //skull noise here
    }
}
