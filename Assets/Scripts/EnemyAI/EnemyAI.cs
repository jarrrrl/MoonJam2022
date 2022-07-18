using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float health = 10f;
    public HealthController healthController;
    protected bool facingRight = true;
    protected Transform playerTransform;
    protected bool isActive = false;
    protected float activationDistance = 10f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        healthController = GetComponent<HealthController>();
    }

    void FixedUpdate()
    {
        Flip();
        if (Vector3.Distance(transform.position, playerTransform.position) < 10f)
        {
            isActive = true;
        }
        else isActive = false;
    }

    public virtual void Flip()
    {
        if (isActive)
        {
            if (transform.position.x < playerTransform.position.x && facingRight
                || transform.position.x > playerTransform.position.x && !facingRight)
            {
                facingRight = !facingRight;
                Vector3 tempVec = transform.localScale;
                tempVec.x *= -1;
                transform.localScale = tempVec;
            }
        }
    }
    
}
