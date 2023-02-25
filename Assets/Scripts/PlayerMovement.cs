using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] Camera mainCamera;

    Vector2 movement;

    // Update is called once per frame
    private void Start()
    {
        Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseWorldPosition.z = 0f;
        animator.SetFloat("Horizontal", mouseWorldPosition.x);
    }

    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}