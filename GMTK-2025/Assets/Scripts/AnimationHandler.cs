using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;
    public UnitInstance unitInstance;

    public void playAttackAnimation(bool newState)
    {
        bool isRanged = unitInstance.archetype.attackType == ATTACK_TYPE.Ranged || unitInstance.archetype.attackType == ATTACK_TYPE.Ranged_AOE;
        string variableName = isRanged ? "isAttackingRange" : "isAttackingMelee";
        animator.SetBool(variableName, newState);
    }

    public void playDeathAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public void playWalkingAnimation(bool newState)
    {
        animator.SetBool("isWalking", newState);
    }
}
