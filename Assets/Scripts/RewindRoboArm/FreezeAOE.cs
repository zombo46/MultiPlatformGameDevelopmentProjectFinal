using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAOE : MonoBehaviour
{
    public float duration = 0.6f;

    private void OnTriggerEnter(Collider other) { 
        if (other.CompareTag("Enemy")) { 
            var enemy = other.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null) { 
                enemy.Freeze(duration);
            }
        }
    }

    private void Start() { 
        Destroy(gameObject, duration);
    }
}
