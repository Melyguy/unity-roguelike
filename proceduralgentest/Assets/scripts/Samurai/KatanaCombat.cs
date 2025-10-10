using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaCombat : MonoBehaviour
{
    public GameObject Sword;

    public bool canattack = true;

    public Animator anim;

    //public float animwait = 1,5f;

    public float Atckcd = 1.0f;
    public bool isAttacking = false;
    public float attacktime = 0.3f;
    public float combocd = 0.3f;
    public int attacks = 0;
    public AudioSource slash1SFX;
    public AudioSource slash2SFX;
    public bool giveStatusEffect = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {            //left click
            if (canattack == true && attacks == 0)
            {
                SwordAtck();
                attacks = 1;
            }
            else if (canattack == true && attacks == 1)
            {
                SwordAtck2();
                attacks = 0;
            }

        }



    }


    public void SwordAtck()
    {
        canattack = false;
        anim.SetTrigger("attack");
        StartCoroutine(ResetCombocld());
        isAttacking = true;
        slash1SFX.Play();



    }
    public void SwordAtck2()
    {
        canattack = false;
        anim.SetTrigger("attack2");
        StartCoroutine(ResetCombocld());
        isAttacking = true;
        slash2SFX.Play();

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
