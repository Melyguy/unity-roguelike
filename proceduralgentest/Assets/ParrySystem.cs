using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public Animator animator;
    public stanceChange currStance;
    

    void Update()
    {
        if (currStance.currentStance == "Defence")
        {
            if (Input.GetMouseButtonDown(1)) // Right mouse button for parry
            {
                animator.SetTrigger("parry");
            }
        }
    }
}
