using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;
    [SerializeField] private DoorToggle lvl2door;
    
    bool isHovering;
    public float timeToWait = 3f;
    float timeLeft;

    private void Awake()
    {
        timeLeft = timeToWait;
    }

    void OnMouseOver()
    {
        isHovering = true;
    }
     
    void OnMouseExit()
    {
        isHovering = false;
        timeLeft = timeToWait;
        lvl2door.CloseDoor();
    }
     
    void Update()
    {
        if (isHovering)
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
