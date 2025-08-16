using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset lightingPreset;
    [SerializeField, Range(0,24)] private float TimeOfDay;

    public bool isNight = false;
    public float secondsInDay = 600f;


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightingPreset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightingPreset.fogColor.Evaluate(timePercent);
        if (DirectionalLight != null)
        {
            DirectionalLight.color = lightingPreset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.rotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
        }

    }
    private void Update()
    {
        if (lightingPreset == null || DirectionalLight == null)
        {
            return;
        }
        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / secondsInDay;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        if (TimeOfDay >= 20 || TimeOfDay <= 6)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }
        void OnValidate()
    {
        if (DirectionalLight != null) { 
            return;
        }
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            DirectionalLight = FindObjectOfType<Light>();
            foreach (Light light in FindObjectsOfType<Light>())
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
}
