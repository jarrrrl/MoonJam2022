using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.Users;

public class PlayerController : MonoBehaviour
{
    #region serialized fields
    [SerializeField] private float horSpeed = 4f;
    [SerializeField] private float verSpeed = 3f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float coyoteTime = 0.3f;
    [SerializeField] private float fallSpeedClamp = -10f;
    [SerializeField] private float stopJumpMultiplier = 0.9f;
    [SerializeField] private float wallJumpMultiplier = 3f;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private UnityEvent<Vector2, float> jumpEvent;
    #endregion
    #region  public fields
    public enum State
    {
        grounded,
        air,
        climbing
    }
    public State currentState;
    public float wallJumpDir;
    #endregion
    #region  private fields
    private new Rigidbody2D rigidbody2D;
    private HealthController healthController;
    private Vector2 MoveV;
    private Vector2 verBox = new(0.1f, 0.85f);
    private Vector2 horBox = new(0.5f, 0.1f);
    private float timer;
    private bool stopJump;
    private float defaultGravityScale;
    private float smootDampVar;
    private float halfWidth;
    private float halfHeight;
    private Animator animator;
    #endregion
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        healthController = gameObject.GetComponent<HealthController>();
        currentState = State.grounded;
        timer = coyoteTime;
        stopJump = false;
        wallJumpDir = 0f;
        defaultGravityScale = rigidbody2D.gravityScale;
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        halfWidth = gameObject.GetComponent<BoxCollider2D>().size.x / 2f;
        halfHeight = gameObject.GetComponent<BoxCollider2D>().size.y / 2f;
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        FlipSprite();
    }
    void FixedUpdate()
    {
        animator.SetFloat("Velocity", Mathf.Abs(MoveV.x));
        switch (currentState)
        {
            case State.grounded:
                timer = coyoteTime;
                rigidbody2D.velocity = new Vector2(MoveV.x * horSpeed, rigidbody2D.velocity.y);
                animator.SetBool("Grounded", true);
                animator.SetBool("AirOrClimbing", false);
                break;
            case State.air:
                animator.SetBool("Grounded", false);
                animator.SetBool("AirOrClimbing", true);
                rigidbody2D.gravityScale = defaultGravityScale;
                rigidbody2D.velocity = new Vector2(Mathf.SmoothDamp(rigidbody2D.velocity.x, MoveV.x * horSpeed, ref smootDampVar, smoothTime),
                Mathf.Clamp(rigidbody2D.velocity.y, fallSpeedClamp, 100f));

                if (rigidbody2D.velocity.y > 0 && stopJump == true)
                    rigidbody2D.velocity = new Vector2(Mathf.SmoothDamp(rigidbody2D.velocity.x, MoveV.x * horSpeed, ref smootDampVar, smoothTime),
                    rigidbody2D.velocity.y * stopJumpMultiplier);

                if (timer > 0f)
                    timer -= Time.fixedDeltaTime;
                break;
            case State.climbing:
                animator.SetBool("Grounded", false);
                animator.SetBool("AirOrClimbing", true);
                timer = coyoteTime;
                rigidbody2D.velocity = new Vector2(MoveV.x * horSpeed, MoveV.y * verSpeed);
                break;
        }
        ChangeState();
    }
    private void ChangeState()
    {
        wallJumpDir = 0f;
        Collider2D groundCol = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - halfHeight), horBox, 0f, wallLayer);
        Collider2D leftCol = Physics2D.OverlapBox(new Vector2(transform.position.x - halfWidth, transform.position.y), verBox, 0f, wallLayer);
        Collider2D rightCol = Physics2D.OverlapBox(new Vector2(transform.position.x + halfWidth, transform.position.y), verBox, 0f, wallLayer);
        switch (currentState)
        {
            case State.grounded:
                {
                    if (groundCol == null)
                        currentState = State.air;
                }
                break;
            case State.air:
                {
                    switch (rigidbody2D.velocity.x)
                    {
                        case > 0.1f:
                            if (rightCol != null)
                            {
                                rigidbody2D.gravityScale = 0f;
                                currentState = State.climbing;
                            }
                            break;
                        case < -0.1f:
                            if (leftCol != null)
                            {
                                rigidbody2D.gravityScale = 0f;
                                currentState = State.climbing;
                            }
                            break;
                        default:
                            break;
                    }
                    if (groundCol != null)
                    {
                        rigidbody2D.gravityScale = defaultGravityScale;
                        currentState = State.grounded;
                    }
                }
                break;
            case State.climbing:
                if (leftCol == null && rightCol == null)
                {
                    rigidbody2D.gravityScale = defaultGravityScale;
                    currentState = State.air;
                    break;
                }

                switch (rigidbody2D.velocity.x)
                {
                    case > 0.1f:
                        if (leftCol != null)
                        {
                            rigidbody2D.gravityScale = defaultGravityScale;
                            currentState = State.air;
                            break;
                        }
                        wallJumpDir = -1f;
                        break;
                    case < -0.1f:
                        if (rightCol != null)
                        {
                            rigidbody2D.gravityScale = defaultGravityScale;
                            currentState = State.air;
                            break;
                        }
                        wallJumpDir = 1f;
                        break;
                    default:
                        rigidbody2D.gravityScale = defaultGravityScale;
                        currentState = State.air;
                        break;
                }
                break;
        }
    }
    private void FlipSprite()
    {
        if (rigidbody2D.velocity.x > 0.01f)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (rigidbody2D.velocity.x < -0.01f)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        bool stateSwitch = newGameState == GameState.GamePlay;
        enabled = stateSwitch;
        gameObject.GetComponent<Rigidbody2D>().simulated = stateSwitch;
        gameObject.GetComponent<HealthController>().enabled = stateSwitch;
        gameObject.GetComponentInChildren<AttackController>().enabled = stateSwitch;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnDrawGizmos()
    {
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y - halfHeight);
        Vector2 leftPos = new Vector2(transform.position.x - halfWidth, transform.position.y);
        Vector2 rightPos = new Vector2(transform.position.x + halfWidth, transform.position.y);
        Gizmos.DrawWireCube(groundPos, horBox);
        Gizmos.DrawWireCube(leftPos, verBox);
        Gizmos.DrawWireCube(rightPos, verBox);
    }
    #region Input events
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stopJump = false;
            switch (currentState)
            {
                case State.grounded:
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
                    jumpEvent.Invoke(transform.position, wallJumpDir);
                    break;
                case State.air:
                    if (timer > 0f)
                    {
                        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
                        jumpEvent.Invoke(transform.position, wallJumpDir);
                    }
                    break;
                case State.climbing:
                    if (wallJumpDir != 0f)
                    {
                        rigidbody2D.velocity = new Vector2(wallJumpDir * horSpeed * wallJumpMultiplier, jumpVelocity);
                        jumpEvent.Invoke(transform.position, wallJumpDir);
                    }
                    break;
            }
            currentState = State.air;
            timer = 0f;
        }
        if (context.canceled)
        {
            stopJump = true;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        MoveV = context.ReadValue<Vector2>();
    }
    #endregion
}
