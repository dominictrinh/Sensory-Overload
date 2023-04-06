using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;
    [SerializeField] private DoorToggle lvl2door;
    [SerializeField] private ScannerPlayerBounds boundsScript;

    private bool isMouseOver;
    private bool isPlayerOver;
    public float timeToWait = 3f;
    float timeLeft;

    private void Awake()
    {
        timeLeft = timeToWait;
    }

    void OnMouseOver()
    {
        isMouseOver = true;
    }
     
    void OnMouseExit()
    {
        isMouseOver = false;
        timeLeft = timeToWait;
        lvl2door.CloseDoor();
    }

    void Update()
    {
        if (!boundsScript.isPlayerOver)
        {
            timeLeft = timeToWait;
            lvl2door.CloseDoor();
        }

        if (isMouseOver && boundsScript.isPlayerOver)
        {
            timeLeft-=Time.deltaTime;
        }
        if (timeLeft < 0)
        {
            door.OpenDoor();
            lvl2door.OpenDoor();
        }
    }
}
