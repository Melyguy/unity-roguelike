using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressKeyOpenDoor : MonoBehaviour
{

    public GameObject Door;
    public GameObject instruct;
    public GameObject Player;
    public AudioSource audioSource;
    public TextMeshProUGUI ins2;
    public int XPGain = 15;
    public bool harvestable = true;
    public LightingManager lightingManager;
    public bool actions = false;
    public bool opened = false;
    public bool actions2 = false;
    public bool neutral = true;
    // Start is called before the first frame update
    void Start()
    {
        instruct.SetActive(false);
        lightingManager = FindObjectOfType<LightingManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        actions = true;
        instruct.SetActive(true);

    }

    private void OnTriggerExit(Collider col)
    {
        instruct.SetActive(false);
        actions = false;
        ins2.enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && harvestable == true)
        {
            if (actions == true)
            {
                ReserachLevelHandler ResarchHandler = FindObjectOfType<ReserachLevelHandler>();
                instruct.SetActive(false);
                //Door.GetComponent<Animator>().Play("DoorOpen");
                actions = false;
                actions2 = true;
                audioSource.Play();
                opened = true;
                ResarchHandler.crystals += 1;
                ResarchHandler.XP += XPGain;
                //Door.GetComponent<Animator>().Play("door");
                Debug.Log("sigma");
            }

        }
        if (lightingManager.isNight)
        {
            harvestable = false;
            instruct.SetActive(false);
        }
        else
        {
            harvestable = true;
        }
    }
}
