using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseIndicator : MonoBehaviour
{
    [SerializeField] private SenseSwitcher senseSwitchScript;

    [SerializeField] private GameObject visionIcon;
    [SerializeField] private GameObject hearingIcon;
    [SerializeField] private GameObject smellIcon;
    
    // Update is called once per frame
    void Update()
    {
        visionIcon.SetActive(senseSwitchScript.currentSense == SenseSwitcher.Sense.Vision);
        hearingIcon.SetActive(senseSwitchScript.currentSense == SenseSwitcher.Sense.Hearing);
        smellIcon.SetActive(senseSwitchScript.currentSense == SenseSwitcher.Sense.Smell);
    }
}
