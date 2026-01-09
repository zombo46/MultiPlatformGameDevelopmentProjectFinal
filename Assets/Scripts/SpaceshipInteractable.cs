using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceshipInteractable : MonoBehaviour, IInteractable
{
    /* 
     * This script implements the IInteractable interface to create an interactable component
     * This component checks if the player has a specific item in their inventory when they interact with it if they do, it triggers a win condition event.
     * A text prompt is displayed when the player is in range to indicate they can interact with the component.
     */

    [Header("Appearance")]
    public GameObject prompt;

    [Header("other")]
    public string[] requiredItemID = {"Lava", "Maze", "Crator"};
    public UnityEvent onWinGame;

    private bool isPlayerInRange = false;
    private PlayerInventory playerInventory;

    void Start()
    {
        if (prompt != null) prompt.SetActive(false);
    }

    private void Update()
    {

    }
    public void Interact(Collider collider)
    {
        if (playerInventory == null)
            playerInventory = FindFirstObjectByType<PlayerInventory>();

        if (playerInventory == null)
            return;

        foreach (string component in requiredItemID)
        {
            if (!playerInventory.HasItem(component))
            {
                Debug.Log("Required Item missing: " + component);
                return; // stop immediately if any are missing
            }
        }

        Debug.Log("you won");
        onWinGame?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (prompt != null) prompt.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (prompt != null) prompt.SetActive(false);
        }
    }

}
