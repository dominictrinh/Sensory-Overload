using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Object = System.Object;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class Particle : MonoBehaviour
{
    [SerializeField] private Transform transform;
    [SerializeField] private Vector2 initVelocity;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float lifetime;

    [SerializeField] private int bounceLimit;
    private int _bounces;

    // Runs when project starts
    private void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        // rb.velocity = initVelocity
        // Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>(col.contactCount);

        int numContacts = col.GetContacts(contacts);

        float contX = 0;
        float contY = 0;
        foreach (ContactPoint2D contact in contacts)
        {
            contX += contact.normal.x;
            contY += contact.normal.y;
        }

        contX /= numContacts;
        contY /= numContacts;
        
        Vector2.Reflect(rb.velocity, new Vector2(contX, contY));
        _bounces++;
        if (_bounces > bounceLimit)
        {
            rb.velocity = Vector2.zero;
            Destroy(gameObject, lifetime);
        }
    }
}
