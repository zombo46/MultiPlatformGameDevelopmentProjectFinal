using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairComponentLava : MonoBehaviour, IInteractable
{
    public void Interact(Collider collider)
    {
        Destroy(gameObject);
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
