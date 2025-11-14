using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaCombat : MonoBehaviour
{
    public GameObject Sword;
    public GameObject M2Hitbox;

    public bool canattack = true;
    public bool canattack2 = true;

    public Animator anim;

    public movement PM;

    //public float animwait = 1,5f;

    public float Atckcd = 1.0f;
    public bool isAttacking = false;
    public float attacktime = 0.3f;
    public float combocd = 0.3f;
    public int attacks = 0;

    public bool isAttacking2 = false;
    public float attacktime2 = 0.4f;
    public float Atckcd2 = 5.0f;

    public AudioSource slash1SFX;
    public AudioSource slash2SFX;
    public bool giveStatusEffect = false;

    void start()
    {
        PM = FindObjectOfType<movement>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {            //left click
            if (canattack == true && attacks == 0 && PM.grounded == true)
            {
                SwordAtck();
                attacks = 1;
            }
            else if(canattack2 == true && PM.grounded == false)
            {
                SwordJumpAtck();
                attacks = 0;
            }
            else if (canattack == true && attacks == 1)
            {
                SwordAtck2();
                attacks = 0;
            }

        }
        if (Input.GetMouseButtonDown(1))
        {   //right click
            if (canattack2 == true && PM.grounded == true)
            {
                M2Atck();
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
        PM.enabled = false;
        anim.SetBool("Running", false);



    }
    public void SwordAtck2()
    {
        canattack = false;
        anim.SetTrigger("attack2");
        StartCoroutine(ResetCombocld());
        isAttacking = true;
        slash2SFX.Play();
        PM.enabled = false;
        anim.SetBool("Running", false);

    }
    public void SwordJumpAtck()
    {
        canattack2 = false;
        anim.SetTrigger("JumpAttack");
        StartCoroutine(ResetAtckcld2());
        isAttacking2 = true;
        slash1SFX.Play();
    }
    public void M2Atck()
    {
        canattack2 = false;
        anim.SetTrigger("M2attack");
        StartCoroutine(ResetAtckcld2());
        isAttacking2 = true;
        slash1SFX.Play();
        M2Hitbox.SetActive(true);
        PM.enabled = false;
        anim.SetBool("Running", false);
    }


    IEnumerator ResetAtckcld()
    {
        StartCoroutine(ResetAtckBool());
        yield return new WaitForSeconds(Atckcd);
        canattack = true;
        PM.enabled = true;
    }
    IEnumerator ResetAtckcld2()
    {
        StartCoroutine(ResetAtckBool2());
        yield return new WaitForSeconds(Atckcd2);
        canattack2 = true;
        PM.enabled = true;
    }
    IEnumerator ResetCombocld()
    {
        StartCoroutine(ResetComboBool());
        yield return new WaitForSeconds(Atckcd);
        canattack = true;
        PM.enabled = true;
    }
    IEnumerator ResetAtckBool()
    {
        yield return new WaitForSeconds(attacktime);
        isAttacking = false;

    }
    IEnumerator ResetAtckBool2()
    {
        yield return new WaitForSeconds(attacktime);
        isAttacking2 = false;
        canattack = true;
        PM.enabled = true;
    }
    IEnumerator ResetComboBool()
    {
        yield return new WaitForSeconds(combocd);
        isAttacking = false;
    }
}
