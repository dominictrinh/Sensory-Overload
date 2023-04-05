using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterTrigger : MonoBehaviour
{
    [SerializeField] private EmitAudioParticle a;
    [SerializeField] private EmitAudioParticle b;
    [SerializeField] private EmitAudioParticle c;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Particle"))
        {
            a._cooldown = 0;
            b._cooldown = 0;
            c._cooldown = 0;
        }
    }
}
