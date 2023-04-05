using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPressurePlate : MonoBehaviour
{
    [SerializeField] private List<PressurePlate> plateScripts;
    [SerializeField] private DoorToggle door;

    private bool closed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PressurePlate script in plateScripts)
        {
            if (script.isGrid)
            {
                if (!script.isActivated)
                {
                    closed = true;
                }
            }
        }

        if (!closed)
        {
            door.OpenDoor();
        }
        else
        {
            door.CloseDoor();
        }
            
    }
}
