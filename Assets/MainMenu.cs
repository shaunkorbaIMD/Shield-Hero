using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioClip selectSound;
    AudioSource audioSource;

    void Update()
    {
        audioSource = GetComponent<AudioSource>();
    }//Update end

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0F)
    {

    }

    public void MenuSelectClick()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
    }
    public void PlayGame ()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("Level_1");
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        Debug.Log("quit");
        Application.Quit();
    }
    public void LoadTutorial()
    {
        audioSource.PlayOneShot(selectSound, 0.9f);
        SceneManager.LoadScene("tutorial_preScene");
    }
    public void PlayTutorialScene()
    {
        SceneManager.LoadScene("tutorial");
        
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
