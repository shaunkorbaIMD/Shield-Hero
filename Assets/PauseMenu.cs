using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public GameObject InGameMenuUI;

    public AudioClip selectSound;
    AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        audioSource = GetComponent<AudioSource>();


    }//Update end

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0F)
    {

    }

    public void Resume()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        PauseMenuUI.SetActive(false);
        InGameMenuUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        PauseMenuUI.SetActive(true);
        InGameMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        Debug.Log("Quitting game...");
        Application.Quit();
    }


}
