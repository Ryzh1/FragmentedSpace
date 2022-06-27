using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu_Controller : MonoBehaviour
{


    public void ExitGame()
    {
        Application.Quit();
    }


    public void LevelSelect(int i)
    {
        SceneManager.LoadScene(i);
    }


}

