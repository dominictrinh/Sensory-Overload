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

    public List<string> passwords;

    bool displayPasswordScreen = false;
    private bool displayCorrectness = false;
    private bool passwordCorrect = false;
    // public string password = "clock";
    // public string password2 = "1245";
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
            
            // passwordToEdit = GUI.PasswordField(new Rect(87, 20, 100, 20), passwordToEdit, "*"[0], 25);
            passwordToEdit = GUI.TextField(new Rect(87, 20, 100, 20), passwordToEdit, 25);

            bool submitField = GUI.Button(new Rect(197, 20, 54, 20), "Submit");

            if (Input.GetKeyDown(KeyCode.Return) || submitField)
            {
                foreach (string password in passwords)
                {
                    if (passwordToEdit == password)
                    {
                        door.OpenDoor();
                        passwordCorrect = true;
                        break;
                    }
                }
                displayCorrectness = true;
            }

            if (displayCorrectness)
            {
                if (passwordCorrect)
                {
                    GUI.Label(new Rect(20, 40, 144, 20), "Password Correct!");
                }
                else
                {
                    GUI.Label(new Rect(20, 40, 144, 20), "Password Incorrect!");
                }
            }
        }
    }
}
