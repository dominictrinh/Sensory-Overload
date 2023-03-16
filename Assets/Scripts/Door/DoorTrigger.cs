using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;
    [SerializeField] private DoorToggle door2;

    private bool closed = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            door.OpenDoor();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(door.CloseDoor());
        }

        if (Input.GetKeyDown(KeyCode.F) & closed)
        {
            door2.OpenDoor();
            closed = false;
        }
        if (Input.GetKeyDown(KeyCode.G) & closed == false)
        {
            door2.CloseDoor();
            closed = true;
        }
    }
}
