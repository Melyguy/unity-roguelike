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

    void Update()
    {
        if (!randomized)
        {
            Card1Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card1Name.text = Card1Upgrade.upgradeName;
            Card2Upgrade = Upgrades[Random.Range(0, Upgrades.Length)];
            Card2Name.text = Card1Upgrade.upgradeName;

        }


    }

    public void Card1()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        randomized = false;
    }
    public void Card2()
    {
        CardFrame.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        randomized = false;
    }
}

