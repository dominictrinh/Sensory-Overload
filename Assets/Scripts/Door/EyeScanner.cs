using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EyeScanner : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite activeSprite;

    [Header("Doors")] 
    [SerializeField] private DoorAnimator doorAnimated;
    [SerializeField] private DoorToggle doorToggle;
    [SerializeField] [Tooltip("Bounds for mouseover")] 
    private Collider2D mainBounds;
    [SerializeField] [Tooltip("Bounds for player")] 
    private Collider2D scannerBounds;
    [SerializeField] [Tooltip("Time for player to wait")] [Min(0)]
    private float timeToWait;
    [SerializeField] [Tooltip("Whether the door can stay open")]
    private bool staysOpen;

    
    [Header("Miscellaneous")]
    [SerializeField] private float timeLeft;
    [SerializeField] private GameObject player;
    
    // private vars
    private Collider2D _playerCollider;
    private SpriteRenderer _spriteRenderer;
    private bool _isHovering;
    private bool _playerInCollider;
    public bool _isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerCollider = player.GetComponent<Collider2D>();

        _isHovering = false;
        _playerInCollider = false;
    }

    void Awake()
    {
        timeLeft = timeToWait;
    }

    private void OnMouseOver()
    {
        _isHovering = true;
    }

    private void OnMouseExit()
    {
        _isHovering = false;
        timeLeft = timeToWait;

        if (doorToggle != null && !staysOpen) 
            doorToggle.CloseDoor();

        // TODO: figure out if we want the door to be permanently open
        if (doorAnimated != null && !staysOpen)
            doorAnimated.CloseDoor();
    }

    void OpenDoors()
    {
        _isOpen = true;
        if (doorAnimated != null) 
            doorAnimated.OpenDoor();
        if (doorToggle != null) 
            doorToggle.OpenDoor();
    }

    void CloseDoors()
    {
        _isOpen = false;
        if (doorAnimated != null) 
            doorAnimated.CloseDoor();
        if (doorToggle != null) 
            doorToggle.CloseDoor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        _isHovering = mainBounds.OverlapPoint(mousePos);
        _playerInCollider = scannerBounds.bounds.Intersects(_playerCollider.bounds);

        if (!_isHovering && !_playerInCollider)
        {
            // player not in colliders
            timeLeft = timeToWait;
        }

        if (timeLeft < 0)
        {
            _spriteRenderer.sprite = activeSprite;
            OpenDoors();
        }
        else
        {
            if (!staysOpen)
            {
                // actually close things if the door isn't supposed to stay open
                _spriteRenderer.sprite = inactiveSprite;
                CloseDoors();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isHovering && _playerInCollider)
        {
            Debug.Log($"time left:{timeLeft}");
            timeLeft -= Time.fixedDeltaTime;
        }
    }
}
