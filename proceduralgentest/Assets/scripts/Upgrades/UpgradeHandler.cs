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
    public TextMeshProUGUI Card1Name;
    public TextMeshProUGUI Card2Name;
    private bool randomized = false;
    public UpgradeSO[] Upgrades;
    private UpgradeSO Card1Upgrade;
    private UpgradeSO Card2Upgrade;
    public bool leachingActive = false;
    public float leachingAmount = 1f;

    void Update()
    {
        if (!randomized)
        {
            randomized = true;
            Card1Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card1Name.text = Card1Upgrade.upgradeName;
            Card2Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card2Name.text = Card2Upgrade.upgradeName;
    
        }


    }

    public void Card1()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        randomized = false;
        if (Card1Upgrade.activateleaching && leachingActive == false)
        {
            leachingActive = true;
        }
        else if (leachingActive = true && Card1Upgrade.activateleaching)
        {
            leachingAmount += 2f;
        }
    }
    public void Card2()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        randomized = false;
        if (Card2Upgrade.activateleaching && leachingActive == false)
        {
            leachingActive = true;
        }
        else if (leachingActive = true && Card2Upgrade.activateleaching)
        {
            leachingAmount += 2f;
        }
    }
}

