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
    public Slider Slider;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI OtherText;
    public TextMeshProUGUI crystalText;
    public movement movement;
    public bool isInfected = false;
    public bool isMutated = false;


    //ts is not effective, but idrc
    public GameObject RockHip;
    public GameObject RockHead;
    public GameObject RockShoulder;
    public GameObject RockUArm;
    public GameObject RockUNArm;
    public GameObject RockHand;


    public Animator animator;

    void Start()
    {
        movement = FindObjectOfType<movement>();
    }

    void Update()
    {
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
                movement.moveSpeed += 2f;
                movement.movepeedcontrol += 2f;
                RockHead.SetActive(true);
                RockHip.SetActive(true);
                RockShoulder.SetActive(true);
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
                movement.movepeedcontrol += 5f;
                RockUArm.SetActive(true);
                RockUNArm.SetActive(true);
                RockHand.SetActive(true);

            }
        }

        if(crystals >= 10)
        {
            XP += crystals * 10;
            crystals -= 10;
        }
    }
}
