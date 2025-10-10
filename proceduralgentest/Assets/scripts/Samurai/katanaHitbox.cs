using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaHitbox : MonoBehaviour
{
    public KatanaCombat WC;
    public GameObject HitParticle;
    public float Damage = 25f;
    public float selfHeal = 10f;
    public bool hasHit = false;
    public UpgradeHandler upgradeHandler;
    public playerhealth playerHP;


    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy" && WC.isAttacking && hasHit == false)
        {
            Debug.Log("Hit" + other.name);
            hasHit = true;
            //other.GetCOmponent<Animator>().SetTrigger("Hit");

            Instantiate(HitParticle, new Vector3(other.transform.position.x,
                 transform.position.y, other.transform.position.z), other.transform.rotation);
            enemyHP targetdmg = other.GetComponent<enemyHP>();
            if (targetdmg != null)
            {

                targetdmg.TakeDamage(Damage + upgradeHandler.damageIncrease);
                if (upgradeHandler.leachingActive)
                {
                    playerHP.Heal(selfHeal);
                }
                if (WC.giveStatusEffect)
                {
                    if (WC.attacks == 0)
                    {
                        targetdmg.isPoisoned = true;
                        Debug.Log("Poisoned");
                    }
                    else if (WC.attacks == 1)
                    {
                        targetdmg.isDespaired = true;
                        Debug.Log("Despaired");
                    }


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
