using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private GameObject preview;
    
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
    [SerializeField] private float dropDetectionRadius;
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
    
    private bool RemoveItem(InventoryObject invObj)
    {
        // TODO: figure out this bloody part
        bool removed = inventoryList.Remove(invObj);
        if (removed)
        {
            UpdatePanel();
        }

        return removed;
    }

    public bool InInventory(GameObject obj)
    {
        // does the object have an InventoryObject component
        InventoryObject invObjComponent = obj.GetComponent<InventoryObject>();
        if (invObjComponent != null)
        {
            return inventoryList.Contains(invObjComponent);
        }

        return false; 
    }

    private void Update()
    {
        // if (Input.GetButtonDown("Drop"))
        // {
        //     Debug.Log("switching states!");
        //     dropState = !dropState;
        // }

        // change nothing if paused
        if (Time.timeScale != 0f)
        {
            if (inventoryList.Count > 0)
            {
                int currentItemIndex = inventoryList.IndexOf(currentItem);
                if (Input.GetButtonDown("Inventory Forward"))
                {
                    currentItem.gameObject.SetActive(false);
                    
                    currentItemIndex = (currentItemIndex + 1) % inventoryList.Count;
                    currentItem = inventoryList[currentItemIndex];
                }
                else if (Input.GetButtonDown("Inventory Back"))
                {
                    currentItem.gameObject.SetActive(false);
                    
                    if (currentItemIndex == 0)
                    {
                        currentItemIndex = inventoryList.Count - 1;
                    }
                    else
                    {
                        currentItemIndex -= 1;
                    }

                    currentItem = inventoryList[currentItemIndex];
                }
            }
            
            if (!dropState)
            {
                // switching to drop state if not in drop state
                if (Input.GetButtonDown("Drop"))
                {
                    if (inventoryList.Count > 0)
                    {
                        Debug.Log("switching states!");
                        dropState = !dropState;
                    }
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

                                    foreach (RaycastHit2D hit in results)
                                    {
                                        if (hit.collider.isTrigger)
                                        {
                                            pickupLinecastCount--;
                                        }
                                    }
                                    
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
                pickupDialog.SetActive(false);
                if (inventoryList.Count > 0)
                {
                    // dropping state
                    // making sure the pickup dialog is not visible
                    pickupDialog.SetActive(false);

                    if (currentItem == null)
                    {
                        currentItem = inventoryList[^1];
                    }
                    
                    // getting the current object and it's renderer
                    GameObject currentItemGO = currentItem.gameObject;
                    currentItemGO.SetActive(true);
                    // Sprite invObjImage = currentItem.UIImage;
                    SpriteRenderer invObjImageRenderer = currentItemGO.GetComponent<SpriteRenderer>();
                    // setting the alpha
                    Color color = invObjImageRenderer.color;
                    color.a = dropImgAlpha;
                    invObjImageRenderer.color = color;
                
                    // disabling collision
                    Rigidbody2D currentItemRB = currentItemGO.GetComponent<Rigidbody2D>();
                    currentItemRB.simulated = false;
                
                    // determining mouse position in world space
                    Vector3 mousePos = Input.mousePosition;
                    Vector2 worldPos2D = mainCamera.ScreenToWorldPoint(mousePos);

                    Vector3 gObjectPos = gameObject.transform.position;
                    
                    // get angle of cursor
                    float x = worldPos2D.x - gObjectPos.x;
                    float y = worldPos2D.y - gObjectPos.y;
                    
                    float angleToCursor = (float) Math.Atan(y / x);
                    
                    if (x < 0)
                    {
                        angleToCursor = (float)(Math.Atan(y / x) + Math.PI);
                    }

                    // angle and radius to position
                    float objectX = gObjectPos.x + (float)(dropRadius * Math.Cos(angleToCursor));
                    float objectY = gObjectPos.y + (float)(dropRadius * Math.Sin(angleToCursor));

                    // putting object at the correct position
                    currentItemGO.transform.position = new Vector3(objectX, objectY);
                }
                
                if (Input.GetButtonDown("Drop"))
                {
                    if (currentItem != null)
                    {
                        GameObject currentItemGO = currentItem.gameObject;
                        
                        // check if the object is intersecting anything
                        // check for objects in between
                        ContactFilter2D noFilter = new ContactFilter2D();
                        int overlapCount = Physics2D.OverlapCircle(currentItemGO.transform.position, dropDetectionRadius, noFilter.NoFilter(), overlaps);

                        foreach (Collider2D overlap in overlaps)
                        {
                            if (overlap.isTrigger)
                            {
                                overlapCount--;
                            }
                        }
                        
                        if (overlapCount <= 0)
                        {
                            Debug.Log("No objects in desired position!");
                            // if no objects in between
                            SpriteRenderer invObjImageRenderer = currentItemGO.GetComponent<SpriteRenderer>();
                        
                            // dropping!
                            Rigidbody2D currentItemRB = currentItemGO.GetComponent<Rigidbody2D>();
                            currentItemRB.simulated = true;

                            invObjImageRenderer.color = Color.white;
                            RemoveItem(currentItem);
                            currentItem = null;
                            dropState = !dropState;
                        }
                        else
                        {
                            Debug.Log("Objects intersecting desired position!");
                        }
                    }
                }
            }
        }
    }
}
