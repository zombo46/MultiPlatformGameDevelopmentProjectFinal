using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class RepairComponentLava : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    public void Interact(Collider collider)
    {
        Destroy(gameObject);
        player.GetComponent<PlayerInventory>().AddItem("Artifact");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
