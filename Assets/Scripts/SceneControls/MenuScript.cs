using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro Level");
    }

    public void Retry()
    {
        SceneManager.LoadScene(PreviousLevel.Previous);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void LevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void Intro()
    {
        SceneManager.LoadScene("Intro Level");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
