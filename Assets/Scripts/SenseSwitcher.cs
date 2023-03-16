using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SenseSwitcher : MonoBehaviour
{
    [Header("Objects List")]
    [SerializeField] private List<MonoBehaviour> visionObjects;
    [SerializeField] private List<MonoBehaviour> hearingObjects;
    [SerializeField] private List<MonoBehaviour> smellObjects;

    [Header("Current State")]
    [SerializeField] private bool vision;
    [SerializeField] private bool hearing;
    [SerializeField] private bool smell;

    [Header("Audio Extra")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int audioParticleLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vision") > 0)
        {
            vision = true;
            hearing = false;
            smell = false;
        }

        if (Input.GetAxis("Hearing") > 0)
        {
            vision = false;
            hearing = true;
            smell = false;
        }

        if (Input.GetAxis("Smell") > 0)
        {
            vision = false;
            hearing = false;
            smell = true;
        }

        // Setting objects to enable or disable depending on the active sense
        foreach (MonoBehaviour script in visionObjects)
        {
            script.enabled = vision;
        }

        foreach (MonoBehaviour script in hearingObjects)
        {
            script.enabled = hearing;
        }

        foreach (MonoBehaviour script in smellObjects)
        {
            script.enabled = smell;
        }

        // Extra logic for audio
        if (!hearing)
        {
            mainCamera.cullingMask &= ~(1 << audioParticleLayer);
        }
        else
        {
            mainCamera.cullingMask |= 1 << audioParticleLayer;
        }
    }
}