using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewindable
{
    void OnRewindBeamHit(Vector3 hitPoint);
    void OnRewindBeamExit();
}