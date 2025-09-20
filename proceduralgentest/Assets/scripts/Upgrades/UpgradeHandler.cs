using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeHandler : MonoBehaviour
{

    public movement playerMovement;
    public float damageIncrease = 0;
    private float moveSpeedIncrease;
    public float jumpHeightIncrease;
    public float HealthIncrease;
    private float randDamage;

    public GameObject CardFrame;
    public TextMeshProUGUI Card1Name;
    public TextMeshProUGUI Card2Name;
    public TextMeshProUGUI Card3Name;
    public TextMeshProUGUI Card1Desc;
    public TextMeshProUGUI Card2Desc;
    public TextMeshProUGUI Card3Desc;
    public RawImage card1Image;
    public RawImage card2Image;
    public RawImage card3Image;
    private bool randomized = false;
    public UpgradeSO[] Upgrades;
    private UpgradeSO Card1Upgrade;
    private UpgradeSO Card2Upgrade;
    private UpgradeSO Card3Upgrade;
    public bool leachingActive = false;
    public float leachingAmount = 1f;

    void Update()
    {
        if (!randomized)
        {
            randomized = true;
            Card1Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card1Name.text = Card1Upgrade.upgradeName;
            Card1Desc.text = Card1Upgrade.description;
            card1Image.texture = Card1Upgrade.Icon.texture;
            Card2Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card2Name.text = Card2Upgrade.upgradeName;
            Card2Desc.text = Card2Upgrade.description;
            card2Image.texture = Card2Upgrade.Icon.texture;
            Card3Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card3Name.text = Card3Upgrade.upgradeName;
            Card3Desc.text = Card3Upgrade.description;
            card3Image.texture = Card3Upgrade.Icon.texture;

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
        playerMovement.moveSpeed += Card2Upgrade.moveSpeedIncrease;
        damageIncrease += Card1Upgrade.damageIncrease;
        playerMovement.jumpForce += Card1Upgrade.jumpHeightIncrease;
        HealthIncrease += Card1Upgrade.healthIncrease;

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
        playerMovement.moveSpeed += Card2Upgrade.moveSpeedIncrease;
        damageIncrease += Card2Upgrade.damageIncrease;
        playerMovement.jumpForce += Card2Upgrade.jumpHeightIncrease;
        HealthIncrease += Card2Upgrade.healthIncrease;
    }
    public void Card3()
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
        playerMovement.moveSpeed += Card2Upgrade.moveSpeedIncrease;
        damageIncrease += Card3Upgrade.damageIncrease;
        playerMovement.jumpForce += Card3Upgrade.jumpHeightIncrease;
        HealthIncrease += Card3Upgrade.healthIncrease;
    }
}

