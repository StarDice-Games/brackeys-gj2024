using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;

    private string isWalkingParam = "isWalking";

    private string attackTrigger = "attackTrigger";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetIsMoving(bool isMoving)
    {
        animator.SetBool(isWalkingParam, isMoving);
    }

    public IEnumerator SetAttackTrigger()
    {
        animator.SetTrigger(attackTrigger);
        yield return new WaitForSeconds(1f);
        animator.ResetTrigger(attackTrigger);
    }
}
