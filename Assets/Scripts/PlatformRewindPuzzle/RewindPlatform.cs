using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPlatform : MonoBehaviour, IRewindable
{
    private Rigidbody rb;
    private bool isRewinding = false;
    private float rewindDur = 4f;
    private bool isFloating = false;
    private float FloatTime = 150;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float timer = 0;


    private IEnumerator FloatInTheAir()
    {
       Debug.Log("Floating");
        isFloating = true;
        rb.isKinematic = true;
        yield return new WaitForSeconds(FloatTime);

        if (rb != null) 
        {
            rb.isKinematic = false;
        }
        isFloating = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        
    }

    void Update()
    {
        if (!isRewinding) 
        {
            return;
        }

        timer += Time.deltaTime;
        float a = Mathf.Clamp01(timer / rewindDur);
        transform.position = Vector3.Lerp(transform.position, initialPosition, a);
        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, a);

        if (a >= 1f && !isFloating) 
        {
            StartCoroutine(FloatInTheAir());
            isRewinding = false;
        }
       
    }

    public void OnRewindBeamHit(Vector3 hitPoint)
    {
        if (isFloating || isRewinding) 
        {
            return;
        }
        rb.isKinematic = true;
        timer = 0f;
        isRewinding = true;
    }
    public void OnRewindBeamExit()
    {
        if(!isRewinding) 
        {
            return;
        }
        isRewinding = false;
        rb.isKinematic = false;
    }
}



