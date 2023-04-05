using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyboardClose : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            gameObject.SetActive(false);
        }
    }
}
