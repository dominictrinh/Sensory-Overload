using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Interactable interaction;
    [SerializeField] float interactionDistance;

    [SerializeField] private bool canInteract;

    private Transform _selfTransform;

    void Start()
    {
        canInteract = false;
        _selfTransform = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        // if player within radius, allow interaction
        float distance = Vector3.Distance(player.transform.position, _selfTransform.position);
        // Debug.Log($"Distance = {distance}");
        if (distance <= interactionDistance)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }

        if (Input.GetAxis("Use") != 0)
        {
            if (canInteract)
            {
                Debug.Log($"Interaction from {gameObject.name}");
            }
        }
    }
}
