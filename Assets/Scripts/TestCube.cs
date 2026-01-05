using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IRewindable
{
    public void OnRewindBeamHit(Vector3 HitPoint) 
    { 
        GetComponent<Renderer>().material.color = Color.red;
    }
    public void OnRewindBeamExit() 
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
