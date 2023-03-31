using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField] new Light2D light;

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
        
        float angle = Mathf.Atan2(mouseWorldPosition.y, mouseWorldPosition.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        light.transform.rotation = rotation;
        
        // light.transform.Rotate(0,0,rotation, );
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}