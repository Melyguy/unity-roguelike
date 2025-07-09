using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapoController : MonoBehaviour
{
    public GameObject Sword;
    
    public bool canattack = true;

    public Animator anim;

    public GameObject sword1; 
    public GameObject sword2;
    public bool swordchange = false;
    //public float animwait = 1,5f;

    public float Atckcd = 1.0f;
    public bool isAttacking = false;
    public float attacktime = 0.3f;
    public float combocd = 0.3f;
    public float attacks = 0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)){            //left click
            if (canattack == true && attacks == 0f)
            {
                SwordAtck();
                attacks = 1f;
            }
            else if(canattack == true && attacks == 1f)
            {
                SwordAtck2();
                attacks = 0f;
            }

        }
        if(swordchange == true)
        {
            sword1.SetActive(false);
            sword2.SetActive(true);
        }


    }


    public void SwordAtck()
    {
        canattack = false;
        anim.SetTrigger("attack");
        StartCoroutine(ResetCombocld());
        isAttacking = true;
    }
    public void SwordAtck2()
    {
        canattack = false;
        anim.SetTrigger("attack2");
        StartCoroutine(ResetCombocld());
        isAttacking = true;
    }


    IEnumerator ResetAtckcld()
    {
        StartCoroutine(ResetAtckBool());
        yield return new WaitForSeconds(Atckcd);
        canattack = true;
    }
    IEnumerator ResetCombocld()
    {
        StartCoroutine(ResetComboBool());
        yield return new WaitForSeconds(Atckcd);
        canattack = true;
    }
    IEnumerator ResetAtckBool()
    {
        yield return new WaitForSeconds(attacktime);
        isAttacking = false;
    }
    IEnumerator ResetComboBool()
    {
        yield return new WaitForSeconds(combocd);
        isAttacking = false;
    }
}
