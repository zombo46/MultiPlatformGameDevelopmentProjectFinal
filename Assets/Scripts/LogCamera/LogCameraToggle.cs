using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCameraToggle : MonoBehaviour
{
    [SerializeField] private GameObject logCamera;

    private LogCameraLogic logCameraLogic;

    void Start()
    {
        logCameraLogic = logCamera.GetComponent<LogCameraLogic>();

        logCameraLogic.cameraToggle.AddListener(logCameraLogic.LogCameraSetup);
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("f")))
        {
            logCameraLogic.cameraToggle.Invoke();
        }
    }
}
