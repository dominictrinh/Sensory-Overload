using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Transform))]

public class EmitParticle : MonoBehaviour
{
    [SerializeField] private Transform trans;
    
    [SerializeField] private GameObject origParticle;
    [SerializeField] private int copies;
    [SerializeField] private float particleVel;
    private float _rotationAngle;

    [SerializeField] private float cooldownStart;
    [SerializeField] private float cooldownRate;
    private float _cooldown;
    
    // Start is called before the first frame update
    private void Start()
    {
        trans = GetComponent<Transform>();
        _rotationAngle = (360f / copies) * Mathf.PI / 180f;
        _cooldown = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && _cooldown <= 0)
        {
            // Debug.Log("Jump");

            System.Random rand = new System.Random();
            double offset = rand.NextDouble() * Mathf.PI * 2;
            
            for (int i = 0; i < copies; i++)
            {
                // Debug.Log(string.Format("Particle {0} emitted!", i));
                GameObject particle = Instantiate(origParticle, trans.position, Quaternion.identity);
                Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
                if (particleRb)
                {
                    particleRb.velocity =
                        new Vector2(1 * Mathf.Cos(_rotationAngle * i+ (float) offset),
                            1 * Mathf.Sin(_rotationAngle * i + (float) offset));
                    particleRb.velocity *= particleVel;
                    // Debug.Log(particleRb.velocity);
                }
            }

            _cooldown = cooldownStart;
        }
    }

    private void FixedUpdate()
    {
        _cooldown -= cooldownRate * Time.fixedDeltaTime;
        if (_cooldown < 0)
        {
            _cooldown = 0;
        }
        else
        {
            Debug.Log(string.Format("Cooldown: {0}", _cooldown));
        }
    }
}
