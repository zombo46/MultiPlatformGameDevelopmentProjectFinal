using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairPos : MonoBehaviour
{
    public Camera mainCam;

    // Update is called once per frame
    void Update()
    {
        updateCrosshair();
    }

    void updateCrosshair() { 
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        Vector3 hitPoint = ray.GetPoint(50f);
        Vector3 screen = mainCam.WorldToScreenPoint(hitPoint);
        GetComponent<RectTransform>().position = screen;
    }
}
