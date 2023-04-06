using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerPlayerBounds : MonoBehaviour
{
    public bool isPlayerOver;
    private void OnTriggerEnter2D(Collider2D col)
    {
        isPlayerOver = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isPlayerOver = false;
    }
}
