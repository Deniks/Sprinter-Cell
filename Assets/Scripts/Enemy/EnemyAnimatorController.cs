using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(string variable, bool trigger)
    {
        if (trigger)
        {
            animator.SetBool(variable, true);

        }
        else
        {
            animator.SetBool(variable, false);
        }
    }

    public void PlayTargetAnimation(string targetAnim)
    {
        animator.CrossFade(targetAnim, 0.2f);
    }


}
