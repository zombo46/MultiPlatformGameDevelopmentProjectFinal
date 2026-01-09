using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipPartSlotLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private GameObject glow;

    public void Setup(string partID, PlayerInventory inv)
    {
        if (label != null)
            label.text = partID;

        if (glow != null)
            glow.SetActive(inv != null && inv.HasItem(partID));
    }
}