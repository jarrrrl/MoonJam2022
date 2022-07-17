using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    public void StartDust(Vector2 pos, float dir)
    {
        transform.position = pos;
        switch (dir)
        {
            case 0f:
                transform.right = Vector3.right;
                animator.SetTrigger("StartAnim");
                break;
            case 1f:
                transform.right = Vector3.down;
                animator.SetTrigger("StartAnim");
                break;
            case -1f:
                transform.right = Vector3.up;
                animator.SetTrigger("StartAnim");
                break;
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        bool stateSwitch = newGameState == GameState.GamePlay;
        enabled = stateSwitch;
        gameObject.GetComponent<Animator>().enabled = stateSwitch;
        // gameObject.GetComponent<Rigidbody2D>().simulated = stateSwitch;
        // gameObject.GetComponent<HealthController>().enabled = stateSwitch;
        // gameObject.GetComponentInChildren<AttackController>().enabled = stateSwitch;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
}
