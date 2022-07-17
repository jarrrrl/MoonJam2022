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
        if (!isActive && isPlayerInRange.transform.tag.Equals("Player"))
        {
            StartCoroutine(Jump());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            Destroy(this.gameObject);
    }

    private IEnumerator Jump()
    {
        isActive = true;
        while (true)
        {
            //jump here
            yield return new WaitForSeconds(3f);
        }
    }
}
