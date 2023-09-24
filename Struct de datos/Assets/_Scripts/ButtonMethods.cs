using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwoScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
