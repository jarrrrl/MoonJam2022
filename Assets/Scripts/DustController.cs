using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
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
}
