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
    public TextMeshProUGUI ShroomsText;

    public Animator animator;

    void Start()
    {
        
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

        }
        if (Level >= 10)
        {
            OtherText.text = "Mutation LV:";

        }
    }
}
