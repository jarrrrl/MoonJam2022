using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AudioClip deathClip;
    private HealthController healthController;
    protected bool facingRight = false;
    protected Transform playerTransform;
    public bool isActive = false;
    protected float activationDistance = 10f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        healthController = GetComponent<HealthController>();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    protected virtual void Update()
    {
        Flip();
        if (Vector3.Distance(transform.position, playerTransform.position) < activationDistance)
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
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerTransform.gameObject.GetComponent<HealthController>().ChangeHealth(-1);
        else if (collision.collider.CompareTag("Enemy")) Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
    }


    protected void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        Destroy(gameObject);
    }
    public virtual void KillEntity()
    {
        SoundManager.instance.PlaySound(deathClip);
        Destroy(gameObject);
    }
    protected virtual void OnGameStateChanged(GameState newGameState)
    {
        bool stateSwitch = newGameState == GameState.GamePlay;
        enabled = stateSwitch;
        gameObject.GetComponent<Rigidbody2D>().simulated = stateSwitch;
        gameObject.GetComponent<HealthController>().enabled = stateSwitch;
    }
}
