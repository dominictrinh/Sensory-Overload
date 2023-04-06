using System.Collections;
using System.Collections.Generic;
// using UnityEditor.TextCore.Text;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private DoorToggle door;
    [SerializeField] public bool isGrid;

    public float timeToWait;
    private float timeLeft;
    public bool isActivated;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0f)
            {
                if (!isGrid)
                {
                    door.CloseDoor();
                }
                isActivated = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if (col.GetComponent<PressurePlateObject>() != null)
        {
            Debug.Log("obj collision");
            if (!isGrid)
            {
                door.OpenDoor();
            }
            isActivated = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<PressurePlateObject>() != null)
        {
            timeLeft = timeToWait;
            isActivated = true;
        }
    }
}
