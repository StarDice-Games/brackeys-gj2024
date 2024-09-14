using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;

    private string isWalkingParam = "isWalking";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetIsMoving(bool isMoving)
    {
        animator.SetBool(isWalkingParam, isMoving);
    }
}
