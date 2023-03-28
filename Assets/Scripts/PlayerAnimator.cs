using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    protected Animator animator;
    private int walkHash = Animator.StringToHash("walk");
    private int jumpHash = Animator.StringToHash("jump");

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void WalkAnim(float magnitude) {
        animator.SetFloat(walkHash, magnitude);
    }

    public void JumpAnim() {
        animator.SetTrigger(jumpHash);
    }
}