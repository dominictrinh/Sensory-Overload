using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorInteractPressurePlate : MonoBehaviour
{
    [SerializeField] private DoorAnimated door;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                door.CloseDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // tag checks if it is a player or box that is on the pressure plate
        if (collider.CompareTag("Player") || collider.CompareTag("Box"))
        {
            door.OpenDoor();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        // make sure that any object rigibodys you want to interact with the pressure plate have sleeping mode set to never sleep
        if (collider.CompareTag("Player") || collider.CompareTag("Box"))
        {
            timer = 1f;
        }
    }
}
