using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    public AudioSource audioData;

    // called zero
    void Awake()
    {
        Debug.Log("Awake");
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        audioData.Play(0);
    }

    // called third
    void Start()
    {
        audioData = GetComponent<AudioSource>();

        Debug.Log("started");
        Debug.Log("Start");
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
