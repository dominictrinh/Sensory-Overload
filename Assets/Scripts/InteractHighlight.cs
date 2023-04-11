using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHighlight : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] [Min(0)] private float interactRange;
    
    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) < interactRange)
        {
            // TODO: if player in range, do thing
            Debug.Log($"{gameObject.name} in range of player!");
        } 
    }
}
