using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHP : MonoBehaviour
{
    public float maxHP = 100;
    public float currHP = 0;
    public GameObject enemyobject;
    public float xpGivenOnKill = 50f;
    public ReserachLevelHandler researchHandler;
    public bool isPoisoned;
    public bool isDespaired;
    public GameObject poisonSkull;
    public GameObject despairSkull;
    public Slider Hpbar;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
    }

   
    public void TakeDamage(float amount)
    {
        currHP -= amount;
        if (currHP <= 0f)
        {
            Death();
            Debug.Log("Death");
        }
    }
    void Death()
    {
        Destroy(enemyobject);
        researchHandler.XP += xpGivenOnKill;
    }
    public void Update()
    {
        if(isPoisoned)
        {
            TakeDamage(0.01f);
            poisonSkull.SetActive(true);
            Invoke("poisonedOff", 20f);
        }
        if(isDespaired)
        {
            TakeDamage(0.02f);
            despairSkull.SetActive(true);
            Invoke("despairedOff", 20f);

        }
        Hpbar.maxValue = maxHP;
        Hpbar.value = currHP;
    }
    void poisonedOff()
    {
        isPoisoned = false;
        poisonSkull.SetActive(false);
    }
    void despairedOff()
    {
        isDespaired = false;
        despairSkull.SetActive(false);
    }
}
