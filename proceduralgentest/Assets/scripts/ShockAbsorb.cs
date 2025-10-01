using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ShockAbsorb : MonoBehaviour
{
    public playerhealth ph;
    public float currentDamageAbsorb = 1f;
    public float maxDamageAbsorb = 50f;
    public float damageRelease = 10f;
    public KeyCode HealKey = KeyCode.Q;
    public KeyCode DamageKey = KeyCode.R;

    public GameObject ReleaseHitbox;
    public bool isAttacking = false;

    void Update()
    {
        if(ph.currHealth != ph.maxHealth) // adding damage to absorption
        {
            currentDamageAbsorb = ph.maxHealth - ph.currHealth;
        }


        if(currentDamageAbsorb > maxDamageAbsorb) // clamping
        {
            currentDamageAbsorb = maxDamageAbsorb;
        }
        else if(currentDamageAbsorb < 0f)
        {
            currentDamageAbsorb = 0f;
        }


        if (Input.GetKeyDown(HealKey) && currentDamageAbsorb > 0f) //actions
        {
            ph.Heal(damageRelease);
            currentDamageAbsorb -= damageRelease;
        }
        if(Input.GetKeyDown(DamageKey) && ph.currHealth > maxDamageAbsorb && currentDamageAbsorb > 0f)
        {
            damageRelease = currentDamageAbsorb;
            currentDamageAbsorb = 0f;
            isAttacking = true;
            ReleaseHitbox.SetActive(true);
            Invoke(nameof(StopDamaging), 1.5f);
        }
    }
    private void StopDamaging()
    {
        isAttacking = false;
        ReleaseHitbox.SetActive(false);
    }

}
