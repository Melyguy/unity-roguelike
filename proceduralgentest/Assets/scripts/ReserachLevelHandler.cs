using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReserachLevelHandler : MonoBehaviour
{

    public float XP = 0f;
    public int crystals = 0;
    public int shrooms = 0;
    public float XPToNextLevel = 100f;
    public int Level = 0;
    private int levelStored;
    public Slider Slider;
    public Image SliderFill;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI OtherText;
    public TextMeshProUGUI crystalText;
    public movement movement;
    public bool isInfected = false;
    public bool isMutated = false;
    public bool isEvolved = false;
    public bool canresearch = true;
    private MeleeWeapoController meleeWeaponController;
    private KatanaCombat katanaCombat;
    public GameObject cardFrame;
    public ShockAbsorb shockAbsorb;

    //ts is not effective, but idrc
    public GameObject[] RockParts;


    public Animator animator;

    void Start()
    {
        cardFrame.SetActive(false);
        movement = FindObjectOfType<movement>();
        meleeWeaponController = FindObjectOfType<MeleeWeapoController>();
        katanaCombat = FindObjectOfType<KatanaCombat>();
    }

    void Update()
    {
        if(canresearch == false)
        {
            XP += 1f * Time.deltaTime;
        }
        crystalText.text = crystals.ToString();
        if (XP >= XPToNextLevel)
        {
            XP -= XPToNextLevel;
            Level = Level + 1;
            XPToNextLevel += 50;
            Slider.maxValue = XPToNextLevel;
        }
        Slider.value = XP;
        LevelText.text = Level.ToString();

        if(Level >= 5) {
            OtherText.text = "Infection LV:";
            animator.SetBool("Infected", true);

            if(!isInfected)
            {
                isInfected = true;
                SliderFill.color = Color.green;
                movement.moveSpeed += 2f;
                movement.movepeedcontrol += 2f;
                movement.jumpForce += 4f;
               RockParts[0].SetActive(true);
               RockParts[1].SetActive(true);
               RockParts[2].SetActive(true);
               RockParts[6].SetActive(true);
            }

        }
        if (Level >= 10)
        {
            OtherText.text = "Mutation LV:";
            animator.SetBool("Mutated", true);
            if (!isMutated)
            {
                isMutated = true;
                movement.moveSpeed += 5f;
                SliderFill.color = Color.red;
                movement.movepeedcontrol += 5f;
                movement.jumpForce += 4f;
                RockParts[3].SetActive(true);
                RockParts[4].SetActive(true);
                RockParts[5].SetActive(true);
                meleeWeaponController.enabled = true;
                katanaCombat.enabled = true;
                shockAbsorb.enabled = true;

            }
        }
        if (Level >= 15)
        {
            OtherText.text = "Evolved LV:";
            animator.SetBool("Evolved", true);
            if (!isEvolved)
            {
                isEvolved = true;
                movement.moveSpeed += 5f;
                SliderFill.color = Color.black;
                movement.movepeedcontrol += 5f;
                movement.jumpForce += 4f;
                RockParts[7].SetActive(true);
                RockParts[8].SetActive(true);
                RockParts[9].SetActive(true);
                RockParts[10].SetActive(true);

            }
        }
        if (crystals >= 10)
        {
            XP += crystals * 10;
            crystals -= 10;
        }
        if (Level != levelStored)
        {
            levelStored = Level;
            Debug.Log("Level Up! New Level: " + Level);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cardFrame.SetActive(true);
        }
    }
}
