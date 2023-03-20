using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DoorToggle : MonoBehaviour
{
    [SerializeField] private GameObject door;
    
    public void OpenDoor()
    {
        door.transform.Translate(0.16f,0,0);
    }
    public void CloseDoor()
    {
        door.transform.Translate(-0.16f, 0, 0);
    }
}
