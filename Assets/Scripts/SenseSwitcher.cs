using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class SenseSwitcher : MonoBehaviour
{
    enum Sense
    {
        Vision,
        Hearing,
        Smell
    }
    
    [Header("Objects List")]
    [FormerlySerializedAs("visionObjects")] [SerializeField] private List<MonoBehaviour> visionScripts;
    [FormerlySerializedAs("hearingObjects")] [SerializeField] private List<MonoBehaviour> hearingScripts;
    [FormerlySerializedAs("smellObjects")] [SerializeField] private List<MonoBehaviour> smellScripts;
    [SerializeField] private List<GameObject> visionObjs;
    [SerializeField] private List<GameObject> hearingObjs;
    [SerializeField] private List<GameObject> smellObjs;

    [Header("Current State")] 
    [SerializeField] private Sense currentSense;
    private bool _vision;
    private bool _hearing;
    public bool _smell;

    [Header("Audio Extra")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int audioParticleLayer;
    // [SerializeField] private EmitAudioParticle _audioScript;

    void Start()
    {
        if (currentSense == Sense.Vision)
        {
            _vision = true;
            _hearing = false;
            _smell = false;
        } 
        else if (currentSense == Sense.Hearing)
        {
            _vision = false;
            _hearing = true;
            _smell = false;
        }
        else
        {
            _vision = false;
            _hearing = false;
            _smell = false;
        }
        
        foreach (MonoBehaviour script in visionScripts)
        {
            script.enabled = _vision;
        }

        foreach (MonoBehaviour script in hearingScripts)
        {
            script.enabled = _hearing;
        }

        foreach (MonoBehaviour script in smellScripts)
        {
            script.enabled = _smell;
        }
            
        // game object enable/disable
        foreach (GameObject obj in visionObjs)
        {
            obj.SetActive(_vision);
        }

        foreach (GameObject obj in hearingObjs)
        {
            obj.SetActive(_hearing);
        }

        foreach (GameObject obj in smellObjs)
        {
            if (obj.CompareTag("Paint"))
            {
                obj.GetComponent<ParticleSystemRenderer>().enabled = _smell;
            }
            else
            {
                obj.SetActive(_smell);
            }
            
        }

        // Extra logic for audio
        if (!_hearing)
        {
            mainCamera.cullingMask &= ~(1 << audioParticleLayer);
        }
        else
        {
            mainCamera.cullingMask |= 1 << audioParticleLayer;
        }
        // _audioScript = GetComponent<EmitAudioParticle>();
        // _audioScript.enabled = _hearing;
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = false;

        if (Input.GetButtonDown("Vision"))
        {
            currentSense = Sense.Vision;
            
            _vision = true;
            _hearing = false;
            _smell = false;

            changed = true;
        }

        else if (Input.GetButtonDown("Hearing"))
        {
            currentSense = Sense.Hearing;
            
            _vision = false;
            _hearing = true;
            _smell = false;
            
            changed = true;
        }

        else if (Input.GetButtonDown("Smell"))
        {
            currentSense = Sense.Smell;
            
            _vision = false;
            _hearing = false;
            _smell = true;
            
            changed = true;
        }

        if (changed)
        {
            // Setting objects to enable or disable depending on the active sense
            foreach (MonoBehaviour script in visionScripts)
            {
                Debug.Log($"{script.name} set to {_vision}");
                script.enabled = _vision;
            }

            foreach (MonoBehaviour script in hearingScripts)
            {
                Debug.Log($"{script.name} set to {_hearing}");
                script.enabled = _hearing;
            }

            foreach (MonoBehaviour script in smellScripts)
            {
                Debug.Log($"{script.name} set to {_smell}");
                script.enabled = _smell;
            }
            
            // game object enable/disable
            foreach (GameObject obj in visionObjs)
            {
                obj.SetActive(_vision);
            }

            foreach (GameObject obj in hearingObjs)
            {
                obj.SetActive(_hearing);
            }

            foreach (GameObject obj in smellObjs)
            {
                if (obj.CompareTag("Paint"))
                {
                    if (obj.GetComponent<TilemapRenderer>().enabled)
                    {
                        obj.GetComponent<ParticleSystemRenderer>().enabled = _smell;
                    }
                }
                else
                {
                    obj.SetActive(_smell);
                }
            }

            // Extra logic for audio
            if (!_hearing)
            {
                mainCamera.cullingMask &= ~(1 << audioParticleLayer);
            }
            else
            {
                mainCamera.cullingMask |= 1 << audioParticleLayer;
            }
            // _audioScript.enabled = _hearing;
        }
    }
}