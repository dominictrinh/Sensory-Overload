using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordDoor : MonoBehaviour
{
    [SerializeField] private DoorToggle door;

    [SerializeField] private GameObject player;
    [SerializeField] private float playerDist;

    bool displayPasswordScreen = false;
    public string password = "test";
    public string passwordToEdit = "";

    void Update()
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) < playerDist)
        {
            if (Input.GetButton("Interact"))
            {
                displayPasswordScreen = true;
            }
        }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (Vector2.Distance(player.transform.position, gameObject.transform.position) < playerDist)
        //     {
        //         
        //     }
        //     
        //     RaycastHit raycastHit;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(ray, out raycastHit, 100f))
        //     {
        //         if (raycastHit.transform != null)
        //         {
        //             CurrentClickedGameObject(raycastHit.transform.gameObject);
        //         }
        //     }
        // }

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
            GUI.Label(new Rect(20, 20, 72, 20), "Password: ");
            
            passwordToEdit = GUI.PasswordField(new Rect(87, 20, 100, 20), passwordToEdit, "*"[0], 25);

            bool submitField = GUI.Button(new Rect(197, 20, 54, 20), "Submit");
            

            if ((Input.GetKeyDown(KeyCode.Return) || submitField) && passwordToEdit == password)
            {
                door.OpenDoor();
            }
        }
    }
}
