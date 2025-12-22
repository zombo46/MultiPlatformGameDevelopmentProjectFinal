using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IInteractable
{
    public void Interact(Collider collider)
    {
        Debug.Log("Oh look at me I'm a cube oh my god");
    }
}
