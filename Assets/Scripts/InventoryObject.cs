using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private Sprite uiImage;

    public Sprite UIImage
    {
        get => uiImage;
    }
}
