using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonDetecting : MonoBehaviour
{
    public MeleeWeapoController WC;
    public GameObject HitParticle;
    public float Damage = 100f;


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && WC.isAttacking)
        {
            Debug.Log("Hit" + other.name);
            //other.GetCOmponent<Animator>().SetTrigger("Hit");

            Instantiate(HitParticle, new Vector3(other.transform.position.x,
                transform.position.y, other.transform.position.z), other.transform.rotation);
            enemyHP targetdmg = other.GetComponent<enemyHP>();
            if (targetdmg != null)
            {
                targetdmg.TakeDamage(Damage);
            }


        }
    }
}
