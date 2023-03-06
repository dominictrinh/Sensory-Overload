/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour {

    public void OpenDoor() {
        gameObject.SetActive(false);
    }

    public void CloseDoor() {
        gameObject.SetActive(true);
    }

}
