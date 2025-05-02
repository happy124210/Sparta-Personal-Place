using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsRun = Animator.StringToHash("isRun");
    private static readonly int IsHit = Animator.StringToHash("isHit");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsRun, obj.magnitude > .5f);
    }

    public void Damage()
    {
        animator.SetBool(IsHit, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}