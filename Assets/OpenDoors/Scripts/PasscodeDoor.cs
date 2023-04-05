using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasscodeDoor : MonoBehaviour
{
    [SerializeField] private DoorAnimated door;

    bool displayPasswordScreen = false;
    public string password = "test";
    public string passwordToEdit = "blah";

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            displayPasswordScreen = true;
        }
    }

    void OnGUI()
    {
        if (displayPasswordScreen)
        {
            passwordToEdit = GUI.PasswordField(new Rect(10, 10, 200, 20), passwordToEdit, "*"[0], 25);

            if (Input.GetKeyDown(KeyCode.Return) && passwordToEdit == password)
            {
                door.OpenDoor();
            }
        }
    }
    
}