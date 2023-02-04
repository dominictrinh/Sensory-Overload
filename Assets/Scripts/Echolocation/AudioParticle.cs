using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Object = System.Object;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AudioParticle : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private float _alphaIncrement;

    [Header("Particle Properties")]
    [SerializeField] private float lifetime;
    [SerializeField] private int bounceLimit;
    // [SerializeField] private Vector2 initVelocity;
    private int _bounces;

    // Runs when project starts
    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _alphaIncrement = 1 / lifetime;

        // rb.velocity = initVelocity
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        Color spriteColor = _sprite.color;
        spriteColor.a = spriteColor.a - (_alphaIncrement * Time.fixedDeltaTime);
        _sprite.color = spriteColor;
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
        
        Vector2.Reflect(_rb.velocity, new Vector2(contX, contY));
        _bounces++;
        if (_bounces > bounceLimit)
        {
            _rb.velocity = Vector2.zero;
            // Destroy(gameObject, lifetime);
        }
    }
}
