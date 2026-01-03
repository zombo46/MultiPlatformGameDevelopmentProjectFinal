using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float flowSpeed = 0.5f;
    public float pushStrength = 15f;

    public bool isFrozen = false;

    Material mat;
    Vector2 uvOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update() 
    {
        if (!isFrozen) {
            uvOffset.x += flowSpeed * Time.deltaTime;
            mat.SetTextureOffset("_BaseMap", uvOffset);   // URP
            mat.SetTextureOffset("_MainTex", uvOffset);   // Standard
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!isFrozen && col.CompareTag("Player"))
        {
            CharacterController controller = col.GetComponent<CharacterController>();
            if (controller != null)
            {
                Vector3 pushDir = transform.right * pushStrength * Time.deltaTime;
                controller.Move(pushDir);
            }
        }
    }

    public void Freeze() {
        isFrozen = true;
    }

    public void Unfreeze() {
        isFrozen = false;
    }
}

