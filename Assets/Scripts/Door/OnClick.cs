using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    [SerializeField] private DoorAnimator door;
    
    void OnMouseDown()
    {
         door.OpenDoor();
    }
    
}
