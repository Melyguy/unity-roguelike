using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Upgrade", menuName ="Scriptables/Upgrade", order = 2)]

public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public string description;
    public float damageIncrease;
    public float moveSpeedIncrease;
    public Sprite Icon;

}
