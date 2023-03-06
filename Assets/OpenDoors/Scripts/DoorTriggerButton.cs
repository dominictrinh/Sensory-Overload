using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private DoorAnimated door;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            door.OpenDoor();
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            door.CloseDoor();
        }

    }
}
