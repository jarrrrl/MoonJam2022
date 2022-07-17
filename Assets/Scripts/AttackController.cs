using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private float attackCooldown = 0.5f;
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
            foreach (Collider2D col in colliders){

                //deal damage to every enemy
                //col.gameObject.GetComponent<HealthController>().ChangeHealth(-1);
            }
            timer = attackCooldown;
            //also add swipe animation
        }
    }
}
