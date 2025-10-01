using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stanceChange : MonoBehaviour
{
    public Animator animator;
    public string currentStance = "Offence";
    public MeleeWeapoController meleeWeapoController;
    public KeyCode ChangeKey = KeyCode.Q;
    public movement movementScript;

    void Update()
    {
        if (Input.GetKeyDown(ChangeKey))
        {
            if (currentStance == "Offence")
            {
                currentStance = "Defence";
                animator.SetBool("stance2", true);
                meleeWeapoController.canattack = false;
                movementScript.moveSpeed = 8f;

            }
            else
            {
                currentStance = "Offence";
                animator.SetBool("stance2", false);
                meleeWeapoController.canattack = true;
            }
        }
    }


}
