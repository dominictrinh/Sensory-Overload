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
    
    [SerializeField] private Dictionary<GameObject, bool> objPreviousState;
    [SerializeField] private Dictionary<MonoBehaviour, bool> scriptPreviousState;

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
            scriptPreviousState.Add(script, script.enabled);
            script.enabled = false;
        }
        
        foreach (GameObject obj in objectsToPause)
        {
            // TODO: might not be needed to do this
            // if you're disabling objects during a pause like
            // you sure about that?
            objPreviousState.Add(obj, obj.activeInHierarchy);
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
            script.enabled = scriptPreviousState[script];
        }
        
        foreach (GameObject obj in objectsToPause)
        {
            // TODO: might not be needed to do this
            // if you're disabling objects during a pause like
            // you sure about that?
            obj.SetActive(objPreviousState[obj]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // bool changed = false;
        
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            // changed = true;
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
}
