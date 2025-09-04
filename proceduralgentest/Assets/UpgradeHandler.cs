using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeHandler : MonoBehaviour
{

    public movement playerMovement;
    public float damageIncrease = 0;
    private float moveSpeedIncrease;
    private float randDamage;

    public GameObject CardFrame;
    public TextMeshProUGUI Card1Description;
    public TextMeshProUGUI Card2Description;
    private bool statsRandomized = false;

    void Update()
    {


        if(statsRandomized == false)
        {
            randDamage = Random.Range(1f, 15f);
            moveSpeedIncrease = Random.Range(1f, 3f);
            Card1Description.text = "Increase Movement Speed by " + moveSpeedIncrease.ToString("F1");
            Card2Description.text = "Increase Damage by " + randDamage.ToString("F1");
            statsRandomized = true;
        }
    }

    public void Card1()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMovement.moveSpeed += moveSpeedIncrease;
        statsRandomized = false;
    }
    public void Card2()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        damageIncrease += randDamage;
        statsRandomized = false;
    }
}

