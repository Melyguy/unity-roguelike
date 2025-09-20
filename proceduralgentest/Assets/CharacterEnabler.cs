using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnabler : MonoBehaviour
{
    public GameObject scientist;
    public GameObject samurai;
    void Start()
    {
        if (characterSelect.character == "Scientist")
        {
            scientist.SetActive(true);
            samurai.SetActive(false);
            Debug.Log("Scientist enabled");
        }
        else if (characterSelect.character == "Samurai")
        {
            Debug.Log("Samurai enabled");
            samurai.SetActive(true);
            scientist.SetActive(false);
        }
    }
}
