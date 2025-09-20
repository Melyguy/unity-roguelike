using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class characterSelect : MonoBehaviour
{
    public GameObject currFrame;
    public static string character;
    public TextMeshProUGUI introText;
    public TextMeshProUGUI introDesc;
    public Animator camAnim;

    public void SelectCharacter(GameObject Frame)
    {
        Frame.SetActive(true);
        if (currFrame != null && currFrame != Frame)
        {
            currFrame.SetActive(false);
            currFrame = Frame;
        }
    }
    public void SelectionHandler(string Character)
    {
        character = Character;
        Debug.Log("you have selected " + character);
    }
    public void Update()
    {
        if (character == "Scientist")
        {
            camAnim.SetBool("Char2", false);
            introText.text = "The Scientist";
            introDesc.text = "A brilliant mind with a knack for discovering the secrets of the infection. Though he is smart, he cant seem to grasp staying far enough away to not get infected himself.";
        }
        if (character == "Samurai")
        {
            camAnim.SetBool("Char2", true);
            introText.text = "The Samurai";
            introDesc.text = "When the infection reached her home planet it took her weaker brother. Now she is always looking for her missing half, her missing brother. Her blade has strange properties absorbing the infection to use it to cure others affliction.";
        }
    }
}
