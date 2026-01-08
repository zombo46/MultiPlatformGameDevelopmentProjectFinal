using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Steped on platform.");
        if (other.gameObject.CompareTag("Player"))
        {
            
            transform.parent.GetComponent<PuzzlePlatform>().StepOnPlatform();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Left platform.");
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<PuzzlePlatform>().LeavePlatform();
        }
    }
}
