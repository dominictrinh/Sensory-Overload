using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject invObjectPrefab;
    [SerializeField] private GameObject pickupDialog;

    [Header("Contents")] 
    [SerializeField] private int maxItems;
    [SerializeField] private List<InventoryObject> inventoryList;
    [SerializeField] private InventoryObject currentItem;

    [Header("Other")] 
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float pickupRadius;
    [SerializeField] private float dropRadius;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float dropImgAlpha;
    // [SerializeField] private float cooldown;
    [SerializeField] private List<Collider2D> overlaps;
    [SerializeField] private int overlapMax;
    [SerializeField] private bool dropState;

    // private float _cooldownTime;

    void Start()
    {
        overlaps = new List<Collider2D>(overlapMax);
        // _cooldownTime = 0;
        pickupDialog.SetActive(false);
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

    private bool isFull()
    {
        return inventoryList.Count >= maxItems;
    }
    
    private bool RemoveItem(InventoryObject invObj, Vector2 worldPos)
    {
        // TODO: figure out this bloody part
        bool removed = inventoryList.Remove(invObj);
        if (removed)
        {
            UpdatePanel();
            
            // TODO: place the object back into the game world
            GameObject originalObject = invObj.gameObject;
            originalObject.transform.position = worldPos;
        }

        return removed;
    }

    private void Update()
    {
        // if (Input.GetButtonDown("Drop"))
        // {
        //     Debug.Log("switching states!");
        //     dropState = !dropState;
        // }

        if (!dropState)
        {
            // switching to drop state if not in drop state
            if (Input.GetButtonDown("Drop"))
            {
                Debug.Log("switching states!");
                dropState = !dropState;
            }
            else
            {
                // Checking for items within range of picking up
                // skips check if inventory is full
                float bestDist = float.PositiveInfinity;
                InventoryObject invObjNearest = null;
                
                if (!isFull())
                {
                    ContactFilter2D noFilter = new ContactFilter2D();
                    int overlapCount = Physics2D.OverlapCircle(gameObject.transform.position, pickupRadius, noFilter.NoFilter(), overlaps);
                    // Debug.Log($"Number of objects in range: {overlapCount}");
                
                    foreach (Collider2D overlap in overlaps)
                    {
                        if (overlap != null)
                        {
                            GameObject pickupObj = overlap.gameObject;
                            InventoryObject invObjComponent = pickupObj.GetComponent<InventoryObject>();
                            if (invObjComponent != null)
                            {
                                // check for objects in between
                                List<RaycastHit2D> results = new List<RaycastHit2D>();
                                int pickupLinecastCount =
                                    Physics2D.Linecast(gameObject.transform.position, pickupObj.transform.position, noFilter, results);
                                // if no objects in between
                                if (pickupLinecastCount < 3)
                                {
                                    Vector2 toTargetVector = gameObject.transform.position - pickupObj.transform.position;
                                    if (toTargetVector.sqrMagnitude < bestDist)
                                    {
                                        invObjNearest = invObjComponent;
                                        bestDist = toTargetVector.sqrMagnitude;
                                    }
                                }
                            } 
                        }
                    }
                }
        
                if (invObjNearest != null)
                {
                    pickupDialog.SetActive(true);
                    if (Input.GetButtonDown("Interact"))
                    {
                        AddItem(invObjNearest);
                        pickupDialog.SetActive(false);
                    }
                }
            }

        }
        else
        {
            // dropping state
            // making sure the pickup dialog is not visible
            pickupDialog.SetActive(false);

            // getting the current object and it's image and it's renderer
            Sprite invObjImage = currentItem.UIImage;
            SpriteRenderer invObjImageRenderer = invObjImage.GetComponent<SpriteRenderer>();
            // setting the alpha
            Color color = invObjImageRenderer.color;
            color.a = dropImgAlpha;
            invObjImageRenderer.color = color;
            
            // determining mouse position in world space
            Vector3 mousePos = Input.mousePosition;
            Vector2 worldPos2D = Camera.main.ScreenToWorldPoint(mousePos);
            
            // putting image at the mouse cursor
            
            // 
        }
    }
}
