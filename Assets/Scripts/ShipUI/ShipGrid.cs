using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGrid : MonoBehaviour
{
    [SerializeField] private GameObject shipPartPrefab;
    [SerializeField] private PlayerInventory playerInventory;

    private readonly string[] shipParts = { "Lava", "Maze", "Crator" };

    void Start()
    {
        Rebuild();
    }

    void OnEnable()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        // Clear whatever is currently in the grid
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);

        // Spawn the 3 slots
        foreach (string partID in shipParts)
            DisplayPart(partID);
    }

    private void DisplayPart(string partID)
    {
        GameObject instance = Instantiate(shipPartPrefab, transform);
        instance.transform.localScale = Vector3.one;

        ShipPartSlotLogic slot = instance.GetComponent<ShipPartSlotLogic>();
        if (slot != null)
            slot.Setup(partID, playerInventory);
        else
            Debug.LogError("ShipPartPrefab is missing ShipPartSlotLogic.");
    }
}

