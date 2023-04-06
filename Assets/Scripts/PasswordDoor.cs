using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordDoor : MonoBehaviour
{
    [SerializeField] private DoorToggle door;

    bool displayPasswordScreen = false;
    public string password = "test";
    public string passwordToEdit = "";

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            displayPasswordScreen = false;
        }
    }
    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if(gameObject.name == "Keypad")
        {
            displayPasswordScreen = true;
        }
    }

    void OnGUI()
    {
        if (displayPasswordScreen)
        {
            passwordToEdit = GUI.PasswordField(new Rect(20, 20, 200, 20), passwordToEdit, "*"[0], 25);

            if (Input.GetKeyDown(KeyCode.Return) && passwordToEdit == password)
            {
                door.OpenDoor();
            }
        }
    }
}
