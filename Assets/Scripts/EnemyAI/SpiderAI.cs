using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI
{
    private Rigidbody2D rb;
    private Transform spiderTransform;
    public LayerMask layerMask;
    private Vector2 jumpForce;
    private bool jumpEnabled = false;
    
    public float jumpDistance;
    public float jumpHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spiderTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        RaycastHit2D isPlayerInRange = Physics2D.Linecast(spiderTransform.position, playerTransform.position, layerMask);
        if (isPlayerInRange.collider != null && !jumpEnabled && isPlayerInRange.transform.CompareTag("Player") && isPlayerInRange.distance <= 6.0f)
        {
            StartCoroutine(Jump());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Destroy(this.gameObject);
        else if (collision.collider.CompareTag("Enemy")) Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
    }

    private IEnumerator Jump()
    {
        jumpEnabled = true;
        while (true)
        {
            if (isActive)
            {
                jumpDistance = Random.Range(5f, 8f);
                jumpHeight = Random.Range(5f, 8f);
                jumpForce = new Vector2(jumpDistance, jumpHeight);
                if (facingRight)
                    rb.AddForce(new Vector2(-jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                jumpEnabled = false;
                break;
            }
        }
    }

}
