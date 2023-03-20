using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject invObjectPrefab;

    [Header("Contents")] 
    [SerializeField] private int maxItems;
    [SerializeField] private List<InventoryObject> inventoryList;

    [Header("Other")] 
    [SerializeField] private float pickupRadius;
    [SerializeField] private List<Collider2D> overlaps;
    [SerializeField] private int overlapMax;

    void Start()
    {
        overlaps = new List<Collider2D>(overlapMax);
    }
    
    private void UpdatePanel()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        // TODO: update the panel w/ the items
        foreach (InventoryObject invObj in inventoryList)
        { 
            string objName = $"{invObj.gameObject.name}InvImage";
            GameObject copy = Instantiate(invObjectPrefab, inventoryPanel.transform);
            copy.name = objName;
            copy.transform.SetParent(inventoryPanel.transform);
            Image invImage = copy.GetComponent<Image>();
            invImage.sprite = invObj.UIImage;
        }
        
    }

    private bool AddItem(InventoryObject invObj)
    {
        // TODO: add items to the list 
        bool invFull = inventoryList.Count >= maxItems;

        if (!invFull)
        {
            if (!inventoryList.Contains(invObj))
            {
                inventoryList.Add(invObj);
                UpdatePanel();
            
                invObj.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Trying to add something to inventory which is already inside!");
            }
        }
        else
        {
            Debug.Log("Inventory is full!");
        }

        return !invFull;
    }
    
    private bool RemoveItem(InventoryObject invObj)
    {
        // TODO: yeah this too
        bool removed = inventoryList.Remove(invObj);
        if (removed)
        {
            UpdatePanel();
            
            // TODO: place the object back into the game world
        }

        return removed;
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Interact"))
        {
            ContactFilter2D noFilter = new ContactFilter2D();
            int overlapCount = Physics2D.OverlapCircle(gameObject.transform.position, pickupRadius, noFilter.NoFilter(), overlaps);
            Debug.Log($"Number of objects in range: {overlapCount}");
            
            foreach (Collider2D overlap in overlaps)
            {
                if (overlap != null)
                {
                    GameObject pickupObj = overlap.gameObject;
                    InventoryObject invObjComponent = pickupObj.GetComponent<InventoryObject>();
                    if (invObjComponent != null)
                    {
                        Debug.Log("Trying to pick up an object");
                        AddItem(invObjComponent);
                        break;
                    } 
                }
            }
        }
    }
}
