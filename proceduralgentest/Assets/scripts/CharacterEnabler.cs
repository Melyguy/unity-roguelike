using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnabler : MonoBehaviour
{
    public GameObject scientist;
    public GameObject samurai;

    public GameObject researchBarScientist;
    public GameObject CollectedSamples;
    public GameObject researchBarSamurai;
    void Start()
    {
        if (characterSelect.character == "Scientist")
        {
            scientist.SetActive(true);
            samurai.SetActive(false);
            Debug.Log("Scientist enabled");
            researchBarScientist.SetActive(true);
            researchBarSamurai.SetActive(false);
            CollectedSamples.SetActive(true);
        }
        else if (characterSelect.character == "Samurai")
        {
            Debug.Log("Samurai enabled");
            samurai.SetActive(true);
            scientist.SetActive(false);
            researchBarSamurai.SetActive(true);
            researchBarScientist.SetActive(false);
            CollectedSamples.SetActive(false);

        }
    }
}
