using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartSlotLogic : MonoBehaviour
{
    [SerializeField] private GameObject glow;

    // Called by ShipGrid when the ship tab opens
    public void Setup(string partID, PlayerInventory inventory)
    {
        if (glow == null) return;
        glow.SetActive(inventory != null && inventory.HasItem(partID));
    }
}

