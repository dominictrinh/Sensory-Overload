using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class OnHover : MonoBehaviour
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;
    
    [SerializeField] private DoorAnimator door;
    [SerializeField] private DoorToggle lvl2door;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject scannerBounds;
    // [SerializeField] private GameObject mouseBounds;
    private SpriteRenderer _spriteRenderer;
    
    bool isHovering;
    public float timeToWait = 3f;
    float timeLeft;
    private void Awake()
    {
        timeLeft = timeToWait;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnMouseOver()
    {
        isHovering = true;
    }
     
    void OnMouseExit()
    {
        isHovering = false;
        timeLeft = timeToWait;
        lvl2door.CloseDoor();
    }

    void Update()
    {
        if (scannerBounds == null)
        {
            if (isHovering)
            {
                timeLeft-=Time.deltaTime;
            }
        }
        else
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D scannerCollider = scannerBounds.GetComponent<Collider2D>();
            Collider2D mainCollider = GetComponent<Collider2D>();
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mainCollider.OverlapPoint(mousePos))
            {
                Debug.Log("mouse is hovering over collider!");
                isHovering = true;
            }
            
            if (scannerCollider.bounds.Intersects(playerCollider.bounds))
            {
                Debug.Log("player and scanner colliding!");
            }
            
            if (isHovering && scannerCollider.bounds.Intersects(playerCollider.bounds))
            {
                Debug.Log($"time left:{timeLeft}");
                timeLeft-=Time.deltaTime;
            }
            else
            {
                isHovering = false;
                timeLeft = timeToWait;

                if (_spriteRenderer != null && inactiveSprite != null)
                {
                    _spriteRenderer.sprite = inactiveSprite;
                }
                
                if (lvl2door == null)
                {
                    door.CloseDoor();
                }
                else
                {
                    lvl2door.CloseDoor();
                }
            }
        }
        
        if (timeLeft < 0)
        {
            if (_spriteRenderer != null && activeSprite != null)
            {
                _spriteRenderer.sprite = activeSprite;
            }
            
            if (lvl2door == null)
            {
                door.OpenDoor();
            }
            else
            {
                lvl2door.OpenDoor();
            }
        }
    }
}