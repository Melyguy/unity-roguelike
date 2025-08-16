using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="Ligthing Preset", menuName ="Scriptables/Lighting Preset", order = 1)]

public class LightingPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient fogColor;
    public Gradient DirectionalColor;
}
