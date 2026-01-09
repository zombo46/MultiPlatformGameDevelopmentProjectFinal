using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class RepairComponentLava : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;

    [SerializeField] private string componentType;
    public void Interact(Collider collider)
    {
        Destroy(gameObject);
        player.GetComponent<PlayerInventory>().AddItem(componentType);
    }
}
