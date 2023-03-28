using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {
    protected Animator animator;
    private int walkHash = Animator.StringToHash("walk");
    private int attackHash = Animator.StringToHash("attack");

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void WalkAnim(float magnitude) {
        animator.SetFloat(walkHash, magnitude);
    }

    public void AttackAnim() {
        animator.SetTrigger(attackHash);
    }

}