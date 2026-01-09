using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class ShipPartSlot
    {
        public string itemID;        // must match what PlayerInventory.AddItem uses
        public GameObject glow;      // the highlight/checkmark object to enable
                                     
    }
    public class ShipInventory : MonoBehaviour
    {
        [SerializeField] PlayerInventory playerInventory;
        [SerializeField] List<ShipPartSlot> slots = new();
        void Awake()
        {
            if (playerInventory == null)
                playerInventory = FindFirstObjectByType<PlayerInventory>();
        }
        void OnEnable()
        {
            Refresh();
        }
        public void Refresh()
        {
            if (playerInventory == null) return;

            foreach (var slot in slots)
            {
                bool has = playerInventory.HasItem(slot.itemID);

                if (slot.glow != null)
                    slot.glow.SetActive(has);
            }
        }
    }
}
