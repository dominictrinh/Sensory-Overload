using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public float nextLevelX;
    public float nextLevelY;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.position = new Vector3(nextLevelX, nextLevelY, 0);
        }
    }
}
