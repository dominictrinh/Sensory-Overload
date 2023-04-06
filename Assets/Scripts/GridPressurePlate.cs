using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridPressurePlate : MonoBehaviour
{
    [SerializeField] private List<PressurePlate> plateScripts;
    [SerializeField] private DoorToggle door;

    private int unlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unlock = plateScripts.Count;
        foreach (PressurePlate script in plateScripts)
        {
            if (script.isGrid)
            {
                if (script.isActivated)
                {
                    unlock--;
                }
            }
        }

        if (unlock == 0)
        {
            door.OpenDoor();
        }
        else
        {
            door.CloseDoor();
        }
            
    }
}
