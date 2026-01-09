using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrid : MonoBehaviour
{
    [SerializeField] private GameObject shipPartPrefab;
    [SerializeField] private PlayerInventory playerInventory;

    // Static list of your 4 ship parts
    private readonly string[] shipParts =
    {
        "ship_engine",
        "ship_fuelcell",
        "ship_navigation",
        "ship_hull"
    };

    void OnEnable()
    {
        foreach (string partID in shipParts)
        {
            DisplayPart(partID);
        }
    }

    void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void DisplayPart(string partID)
    {
        GameObject instance = Instantiate(shipPartPrefab, transform);
        instance.transform.localScale = Vector3.one;

        ShipPartSlotLogic slot = instance.GetComponent<ShipPartSlotLogic>();
        slot.SendMessage("Setup", playerInventory);
    }
}

