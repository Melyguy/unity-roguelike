using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarDamager : MonoBehaviour
{
    public ShockAbsorb WC;
    public GameObject HitParticle;
    public float Damage = 25f;
    public bool hasHit = false;
    public UpgradeHandler upgradeHandler;
    public playerhealth playerHP;
    public float KnockbackForce = 10f;
    public float selfHeal = 5f;

    private void OnTriggerStay(Collider other)
    {
        Damage = WC.damageRelease;
        if (other.tag == "Enemy" && WC.isAttacking && hasHit == false)
        {
            Debug.Log("Hit" + other.name);
            hasHit = true;
            //other.GetCOmponent<Animator>().SetTrigger("Hit");

            Instantiate(HitParticle, new Vector3(other.transform.position.x,
                 transform.position.y, other.transform.position.z), other.transform.rotation);
            enemyHP targetdmg = other.GetComponent<enemyHP>();
            Rigidbody targetRB = other.GetComponent<Rigidbody>();
            if (targetRB != null)
            {
                Vector3 awayfromplayer = (other.transform.position - transform.position).normalized;
                targetRB.AddForce(awayfromplayer * KnockbackForce, ForceMode.Impulse);
            }
            if (targetdmg != null)
            {

                targetdmg.TakeDamage(Damage + upgradeHandler.damageIncrease);
                if (upgradeHandler.leachingActive)
                {
                    playerHP.Heal(selfHeal);
                }
            }
            Invoke("HitCooldown", 0.5f);


        }
    }
    private void HitCooldown()
    {
        hasHit = false;
    }
}
