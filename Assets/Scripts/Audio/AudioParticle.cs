using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Object = System.Object;

// [RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AudioParticle : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprite;
    private float _alphaIncrement;
    
    private float _lifetime;
    private int _bounceLimit;
    private int _bounces;

    public void SetLifetime(float newLife)
    {
        this._lifetime = newLife;
    }

    public void SetBounceLimit(int newBounceLimit)
    {
        this._bounceLimit = newBounceLimit;
    }
    
    // Runs when project starts
    private void Start()
    {
        // _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();

        _alphaIncrement = 1 / _lifetime;

        // rb.velocity = initVelocity
        Destroy(gameObject, _lifetime);
    }

    private void FixedUpdate()
    {
        Color spriteColor = _sprite.color;
        spriteColor.a -= (_alphaIncrement * Time.fixedDeltaTime);
        _sprite.color = spriteColor;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>(col.contactCount);
        col.GetContacts(contacts);
        
        Vector2.Reflect(_rb.velocity, contacts[0].normal);
        _bounces++;
        if (_bounces > _bounceLimit)
        {
            _rb.velocity = Vector2.zero;
            // Destroy(gameObject, lifetime);
        }
    }
}
