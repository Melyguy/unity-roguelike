using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset lightingPreset;
    [SerializeField, Range(0,24)] public float TimeOfDay;

    public bool isNight = false;
    public float secondsInDay = 600f;
    public int daysPassed = 0;
    public Material SkyboxDay;
    public Material SkyboxNight;
    public bool Daypassed = false;
    public bool finalBossDefeated = false;
    public GameObject winScreen;
    public AudioSource daymusic;
    public AudioSource NightMusic;


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
            daymusic.Pause();
            if (!NightMusic.isPlaying)
            {
                NightMusic.Play();
            }
            RenderSettings.skybox = SkyboxNight;
            Daypassed = false;
        }
        else
        {
            isNight = false;
            if (!daymusic.isPlaying)
            {
                daymusic.Play();
                NightMusic.Pause();
            }
            if (TimeOfDay < 7 && Daypassed == false)
            {
                Daypassed = true;
                daysPassed++;
            }
            RenderSettings.skybox = SkyboxDay;
        }

        if(daysPassed == 3 && finalBossDefeated == true)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


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
    public void RestartDayCycle()
    {
        TimeOfDay = 0;
        daysPassed = 0;
        finalBossDefeated = false;
        winScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
