using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHP : MonoBehaviour
{
    public float maxHP = 100;
    public float currHP = 0;
    public GameObject enemyobject;


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
    }
}
