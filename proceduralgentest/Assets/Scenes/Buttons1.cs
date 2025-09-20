using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons1 : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void settings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


    public void Quit()
    {
        Debug.Log("you have left! more levels soon!");
        Application.Quit();
    }
    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
