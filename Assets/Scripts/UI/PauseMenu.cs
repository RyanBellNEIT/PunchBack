using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if(isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if(isPaused == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
