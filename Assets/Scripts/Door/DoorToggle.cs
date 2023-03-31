using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DoorToggle : MonoBehaviour
{
    [SerializeField] private GameObject door;
    public float xShift = 0f;
    public float yShift = 0f;
    private bool closed = true;

    public void OpenDoor()
    {
        if (closed)
        {
            door.transform.Translate(0.16f * xShift, 0.16f * yShift, 0);
            closed = false;
        }
    }

    public void CloseDoor()
    {
        if (!closed) {
            door.transform.Translate(-0.16f * xShift, -0.16f * yShift, 0);
            closed = true;
        }
    }
}