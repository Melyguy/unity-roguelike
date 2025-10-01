using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public Animator animator;
    public stanceChange currStance;
    public bool isParrying = false;
    public bool block = false;
    public bool parrySuccess = false;


    void Update()
    {
        if (currStance.currentStance == "Defence")
        {
            if (Input.GetMouseButtonDown(1)) // Right mouse button for parry
            {
                animator.SetBool("parry", true);
                isParrying = true;
                Invoke("endParryAfterDelay", 0.5f); // Parry lasts for 0.5 seconds
                block = true;

            }
            else if (Input.GetMouseButtonUp(1))
            {
                animator.SetBool("parry", false);
                block = false;
                isParrying = false;
            }
        }
        void endParryAfterDelay()
        {
            isParrying = false;
        }
    }
}
