using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintScript : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryScript;
    [SerializeField] private GameObject paintBucketObj;
    
    // Update is called once per frame
    void Update()
    {
        if (inventoryScript.InInventory(paintBucketObj))
        {
            // do stuff here
        }
    }
}
