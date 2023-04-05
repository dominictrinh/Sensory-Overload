using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SenseSwitcher : MonoBehaviour
{
    [Header("Objects List")]
    [FormerlySerializedAs("visionObjects")] [SerializeField] private List<MonoBehaviour> visionScripts;
    [FormerlySerializedAs("hearingObjects")] [SerializeField] private List<MonoBehaviour> hearingScripts;
    [FormerlySerializedAs("smellObjects")] [SerializeField] private List<MonoBehaviour> smellScripts;
    [SerializeField] private List<GameObject> visionObjs;
    [SerializeField] private List<GameObject> hearingObjs;
    [SerializeField] private List<GameObject> smellObjs;

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
        bool changed = false;
        
        if (Input.GetButtonDown("Vision"))
        {
            vision = true;
            hearing = false;
            smell = false;

            changed = true;
        }

        if (Input.GetButtonDown("Hearing"))
        {
            vision = false;
            hearing = true;
            smell = false;
            
            changed = true;
        }

        if (Input.GetButtonDown("Smell"))
        {
            vision = false;
            hearing = false;
            smell = true;
            
            changed = true;
        }

        if (changed)
        {
            // Setting objects to enable or disable depending on the active sense
            foreach (MonoBehaviour script in visionScripts)
            {
                script.enabled = vision;
            }

            foreach (MonoBehaviour script in hearingScripts)
            {
                script.enabled = hearing;
            }

            foreach (MonoBehaviour script in smellScripts)
            {
                script.enabled = smell;
            }
            
            // game object enable/disable
            foreach (GameObject obj in visionObjs)
            {
                obj.SetActive(vision);
            }

            foreach (GameObject obj in hearingObjs)
            {
                obj.SetActive(hearing);
            }

            foreach (GameObject obj in smellObjs)
            {
                obj.SetActive(smell);
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
}