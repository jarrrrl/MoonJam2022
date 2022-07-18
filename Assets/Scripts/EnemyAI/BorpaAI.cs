using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorpaAI : EnemyAI
{
    [SerializeField] private AudioClip shootClip;
    Transform borpaTransform;
    Rigidbody2D rb;
    public LayerMask enemyMask;
    public float speed = 3f;
    float borpaWidth;
    public Transform borpaProjectilePrefab;


    private void Start()
    {
        borpaTransform = this.transform;
        rb = GetComponent<Rigidbody2D>();
        borpaWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void FixedUpdate()
    {
        Vector2 lineCast = borpaTransform.position - borpaTransform.right * borpaWidth;
        Debug.DrawLine(lineCast, lineCast + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCast, lineCast + Vector2.down, enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCast, lineCast - (Vector2)borpaTransform.right * .1f, enemyMask);
        Debug.DrawLine(lineCast, lineCast - (Vector2)borpaTransform.right * .1f);

            if (!isGrounded || isBlocked)
            {
                Vector3 currentRotation = borpaTransform.eulerAngles;
                currentRotation.y += 180;
                borpaTransform.eulerAngles = currentRotation;

                if (!isGrounded && isActive) Shoot();
            }

            Vector2 newVelocity = rb.velocity;
            newVelocity.x = -borpaTransform.right.x * speed;
            rb.velocity = newVelocity;
    }

    private void Shoot()
    {
        Transform borpaProjectile = Instantiate(borpaProjectilePrefab, borpaTransform.position, borpaProjectilePrefab.rotation);
        borpaProjectile.GetComponent<Projectile>().OnShoot(borpaTransform.right.x);
        SoundManager.instance.PlaySound(shootClip);
    }
    // public override void OnDestroy()
    // {
    //     //borpa noise here
    // }

}
