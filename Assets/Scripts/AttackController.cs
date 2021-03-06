using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private AudioClip slashClip;
    private Vector2 boxSize = new(2f, 1f);
    private new Camera camera;
    private new Rigidbody2D rigidbody2D;
    private Vector2 pos;
    private Mouse mouse;
    private float timer;
    void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        camera = Camera.main;
        mouse = Mouse.current;
        timer = 0f;
    }
    void FixedUpdate()
    {
        pos = camera.ScreenToWorldPoint(mouse.position.ReadValue());
        transform.right = pos - (Vector2)transform.position;
        if (timer > 0f)
            timer -= Time.fixedDeltaTime;

        if ((pos - (Vector2)transform.position).x > 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started && timer <= 0f)
        {

            pos = camera.ScreenToWorldPoint(mouse.position.ReadValue());
            pos = pos - (Vector2)transform.position;
            pos.Normalize();
            pos *= 1.5f;

            Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position + pos, boxSize, Vector2.Angle(Vector2.right, pos), enemyLayer);
            foreach (Collider2D col in colliders)
            {

                //deal damage to every enemy
                print("hit");
                col.gameObject.GetComponent<HealthController>().ChangeHealth(-1);
            }
            timer = attackCooldown;
            SoundManager.instance.PlaySound(slashClip);
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
