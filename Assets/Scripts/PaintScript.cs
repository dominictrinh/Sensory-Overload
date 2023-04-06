using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PaintScript : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryScript;
    // [SerializeField] private SenseSwitcher senseScript;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject paintBucketObj;
    [SerializeField] private List<Collider2D> overlaps;
    public float range;
    
    // Update is called once per frame
    void Update()
    {
        if (inventoryScript.InInventory(paintBucketObj))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ContactFilter2D noFilter = new ContactFilter2D();
                int overlapCount = Physics2D.OverlapCircle(transform.position, range, noFilter.NoFilter(), overlaps);
                foreach (Collider2D overlap in overlaps)
                {
                    if (overlap.CompareTag("Paint"))
                    {
                        overlap.GetComponent<TilemapRenderer>().enabled = true;

                        SenseSwitcher senseScript = player.GetComponent<SenseSwitcher>();
                        if (senseScript.currentSense == SenseSwitcher.Sense.Smell)
                        {
                            overlap.GetComponent<ParticleSystemRenderer>().enabled = true;
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                ContactFilter2D noFilter = new ContactFilter2D();
                int overlapCount = Physics2D.OverlapCircle(transform.position, range, noFilter.NoFilter(), overlaps);
                foreach (Collider2D overlap in overlaps)
                {
                    if (overlap.CompareTag("Paint"))
                    {
                        overlap.GetComponent<TilemapRenderer>().enabled = false;
                        overlap.GetComponent<ParticleSystemRenderer>().enabled = false;
                    }
                }
            }
        }
    }
}
