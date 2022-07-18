using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI
{
    private Rigidbody2D rb;
    private Transform spiderTransform;
    public LayerMask layerMask;
    private Vector2 jumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spiderTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {

        RaycastHit2D isPlayerInRange = Physics2D.Linecast(spiderTransform.position, playerTransform.position, layerMask);
        if (isPlayerInRange.collider != null && !isActive && isPlayerInRange.transform.CompareTag("Player") && isPlayerInRange.distance <= 6.0f)
        {
            StartCoroutine(Jump());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            Destroy(this.gameObject);
    }

    private IEnumerator Jump()
    {
        isActive = true;
        while (isActive)
        {
            jumpForce = new Vector2(Random.Range(5f, 10f), Random.Range(3f, 5f));
            if (facingRight)
                rb.AddForce(new Vector2(-jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f);
        }
    }

}
