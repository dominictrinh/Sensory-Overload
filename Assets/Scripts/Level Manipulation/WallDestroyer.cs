using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject shadowOld;
    [SerializeField] private GameObject shadowNew;

    [SerializeField] private GameObject center;
    [SerializeField] private GameObject player;
    [SerializeField] private float dist;
    public void BreakWall()
    {
        gameObject.SetActive(false);
        shadowOld.SetActive(false);
        shadowNew.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (Vector2.Distance(center.transform.position, player.transform.position) < dist)
            {
                BreakWall();
            }
        }
    }
}
