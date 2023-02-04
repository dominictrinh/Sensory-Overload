using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Transform))]

public class EmitAudioParticle : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera currentCamera;
    private Transform _parentTransform;
    
    [Header("Particle Info")]
    [SerializeField] private GameObject origParticle;
    [SerializeField] private int copies;
    [SerializeField] private float particleVel;
    [SerializeField] private float degreeSpread;
    [SerializeField] private float cooldownStart;
    [SerializeField] private float cooldownRate;
    
    private float _centerAngleDeg;
    private float _particleOffset; // how much each particle should be separated by, ish

    [FormerlySerializedAs("isSource")]
    [Header("Audio Source")] 
    [SerializeField] private bool isPlayer;
    [SerializeField] private float sourceAngle;
    private float _cooldown;
    
    // Start is called before the first frame update
    private void Start()
    {
        _parentTransform = GetComponent<Transform>();
        _particleOffset = (degreeSpread / copies) * Mathf.PI / 180f;
        _cooldown = 0;

        if (!isPlayer)
        {
            _centerAngleDeg = sourceAngle;
        }
    }

    private void Update()
    {
        if ((!isPlayer && _cooldown <= 0) || (isPlayer && Input.GetKeyDown("space") && _cooldown <= 0))
        {
            float offset = 0;
            if (isPlayer)
            {
                System.Random rand = new System.Random();
                offset = (float) rand.NextDouble() * _particleOffset * (Mathf.PI / 180f);
                
                // changing the center to the cursor's position
                Vector2 cursorPos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
                float x = cursorPos.x - _parentTransform.position.x;
                float y = cursorPos.y - _parentTransform.position.y;
                float angleToCursor = (float) Math.Atan(y / x);

                if (x < 0)
                {
                    angleToCursor = (float)(Math.Atan(y / x) + Math.PI);
                }
                _centerAngleDeg = angleToCursor * (180f / Mathf.PI);
            }
            
            for (int i = 0; i < copies; i++)
            {
                GameObject particle = Instantiate(origParticle, _parentTransform.position, Quaternion.identity);
                Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
                if (particleRb)
                {
                    float angle = (_centerAngleDeg * Mathf.PI / 180f) - (degreeSpread / 2) * (Mathf.PI / 180f) + _particleOffset * i;
                    particleRb.velocity = new Vector2(particleVel * Mathf.Cos(angle + offset),
                        particleVel * Mathf.Sin(angle + offset));
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
            Debug.Log($"Cooldown: {_cooldown}");
        }
    }
}
