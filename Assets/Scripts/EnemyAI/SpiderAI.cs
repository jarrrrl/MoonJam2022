using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : EnemyAI
{
    private Rigidbody2D rb;
    private bool isActive = false;
    private Transform spiderTransform;
    private Transform playerTransform;
    public LayerMask layerMask;
    private Vector2 jumpForce;
    private bool facingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spiderTransform = this.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(spiderTransform.position, playerTransform.position);

        RaycastHit2D isPlayerInRange = Physics2D.Linecast(spiderTransform.position, playerTransform.position, layerMask);
        print(isPlayerInRange.distance);
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
        while (true)
        {
            if (spiderTransform.position.x < playerTransform.position.x && facingRight) Flip();
            if (spiderTransform.position.x > playerTransform.position.x && !facingRight) Flip();

            jumpForce = new Vector2(Random.Range(5f, 10f), Random.Range(3f, 5f));
            //jump here
            if (facingRight)
                rb.AddForce(new Vector2(-jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            yield return new WaitForSeconds(3f);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempVec = spiderTransform.localScale;
        tempVec.x *= -1;
        spiderTransform.localScale = tempVec;
    }
}
