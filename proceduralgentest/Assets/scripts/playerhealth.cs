using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currHealth;
    public GameObject DeathScreen;
    public GameObject cameraOBJ;
    public movement PM;
    public Rigidbody RB;
    public KeyCode RestartKey;
    public Slider HP;
    bool isDead = false;
    void Start()
    {
        currHealth = maxHealth;
        PM = GetComponent<movement>();
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HP.value = currHealth;
        if (Input.GetKeyDown(RestartKey) && isDead == true)
        {
            Debug.Log("hello");
        }
    }
    public void GetHit(float amount)
    {
        currHealth -= amount;
        if (currHealth <= 0f)
        {
            DeathScreen.SetActive(true);
            isDead = true;
            RB.freezeRotation = false;
            //RB.AddForce(transform.right * 100f, ForceMode.Impulse);
            PM.moveSpeed = 0f;
            PM.jumpForce = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;



        }

    }
    public void Heal(float amount)
    {
        currHealth += amount;
        if (currHealth >= 100f)
        {
            currHealth = maxHealth;
        }

    }
}
