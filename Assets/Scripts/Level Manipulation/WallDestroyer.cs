using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject shadowOld;
    [SerializeField] private GameObject shadowNew;
    public void BreakWall()
    {
        gameObject.SetActive(false);
        shadowOld.SetActive(false);
        shadowNew.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BreakWall();
        }
    }
}
