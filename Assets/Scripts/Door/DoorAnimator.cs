using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] private GameObject closedShadow;
    [SerializeField] private Tilemap door1;
    [SerializeField] private Tilemap door2;
    
    private Animator animator;
    private ShadowCaster2D shadow1;
    private ShadowCaster2D shadow2;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        shadow1 = door1.GetComponent<ShadowCaster2D>();
        shadow2 = door2.GetComponent<ShadowCaster2D>();
    }
    public void OpenDoor()
    {
        animator.SetBool("Open", true);
        closedShadow.SetActive(false);
        shadow1.enabled = true;
        shadow2.enabled = true;
    }

    public IEnumerator CloseDoor()
    {
        animator.SetBool("Open", false);
        yield return new WaitForSeconds (2.5f);
        closedShadow.SetActive(true);
        shadow1.enabled = false;
        shadow2.enabled = false;
    }
}
