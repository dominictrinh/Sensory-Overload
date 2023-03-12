using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;

    private bool closed = true;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            door.OpenDoor();
        }
        if (Input.GetMouseButtonDown(1))
        {
            door.CloseDoor();
        }
    }
}
