using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Canvas mainMenu;
    public Canvas aboutMenu;


    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        mainMenu.gameObject.SetActive(false);
        aboutMenu.gameObject.SetActive(true);
    }

    public void Return()
    {
        mainMenu.gameObject.SetActive(true);
        aboutMenu.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
