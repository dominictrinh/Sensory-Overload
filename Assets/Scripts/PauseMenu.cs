using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool paused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> objectsToPause;
    [SerializeField] private List<MonoBehaviour> scriptsToPause;

    void Start()
    {
        paused = false;
    }

    public void Pause()
    {
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        foreach (MonoBehaviour script in scriptsToPause)
        {
            script.enabled = false;
        }
        
        foreach (GameObject obj in objectsToPause)
        {
            // TODO: might not be needed to do this
            // if you're disabling objects during a pause like
            // you sure about that?
            obj.SetActive(false);
        }
    }

    public void Unpause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        
        foreach (MonoBehaviour script in scriptsToPause)
        {
            script.enabled = true;
        }
        
        foreach (GameObject obj in objectsToPause)
        {
            // TODO: might not be needed to do this
            // if you're disabling objects during a pause like
            // you sure about that?
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }
}
