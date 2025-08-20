using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressKeyOpenDoor : MonoBehaviour
{

    public GameObject Door;
    public TextMeshProUGUI instruct;
    public GameObject Player;
    public AudioSource audioSource;
    public TextMeshProUGUI ins2;
    public int XPGain = 15;

    public bool actions = false;
    public bool opened = false;
    public bool actions2 = false;
    public bool neutral = true;
    // Start is called before the first frame update
    void Start()
    {
        instruct.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        actions = true;
        ins2.enabled = true;

    }

    private void OnTriggerExit(Collider col)
    {
        instruct.enabled = false;
        actions = false;
        ins2.enabled = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (actions == true)
            {
                ReserachLevelHandler ResarchHandler = FindObjectOfType<ReserachLevelHandler>();
                instruct.enabled = false;
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
    }
}
