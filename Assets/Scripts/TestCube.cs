using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject textGameObject;
    public void Interact(Collider collider)
    {
        textGameObject.SetActive(true);
        Debug.Log("Oh look at me I'm a cube oh my god");
    }
}
