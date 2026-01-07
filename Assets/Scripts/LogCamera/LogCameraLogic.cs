using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LogCameraLogic : MonoBehaviour
{
    public UnityEvent cameraToggle = new UnityEvent();

    public UnityEvent takePhoto = new UnityEvent();

    [SerializeField] private GameObject logCameraUI;

    [SerializeField] private GameObject gun;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private float photoDistance = 5.0f;

    [SerializeField] private GameObject mainMenu;

    private bool cameraEnabled = false;

    private GameObject hitGameObject;

    void Update()
    {
        if (cameraEnabled)
        {
            CheckCameraView();
            CheckInput();
        }
    }

    public void LogCameraSetup()
    {
        if (mainMenu != null && mainMenu.activeInHierarchy)
        {
            return;
        }
        cameraEnabled = true;
        cameraToggle.RemoveAllListeners();
        gameObject.SetActive(true);
        gun.SetActive(false);
        GetComponentInParent<ProjectileShooter>().enabled = false;
        GetComponentInParent<RewindBeamShooter>().enabled = false;
        logCameraUI.SetActive(true);
        Debug.Log("Log camera setup.");
        cameraToggle.AddListener(LogCameraDisable);
    }

    private void LogCameraDisable()
    {
        if(hitGameObject != null && hitGameObject.GetComponent<Photographable>() != null)
        {
            hitGameObject.SendMessage("HoverCameraExit");
        }

        hitGameObject = null;
        cameraEnabled = false;
        cameraToggle.RemoveAllListeners();
        gameObject.SetActive(false);
        gun.SetActive(true);
        GetComponentInParent<ProjectileShooter>().enabled = true;
        GetComponentInParent<RewindBeamShooter>().enabled = true;
        logCameraUI.SetActive(false);
        Debug.Log("Log camera disabled.");
        cameraToggle.AddListener(LogCameraSetup);
    }

    private void CheckInput()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            takePhoto.Invoke();
        }
    }

    private void CheckCameraView()
    {
        if (!cameraEnabled)
        {
            return;
        }

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

        

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.distance > photoDistance)
            {
                if(hitGameObject != null && hitGameObject.GetComponent<Photographable>() != null)
                {
                    hitGameObject.SendMessage("HoverCameraExit");
                }

                hitGameObject = null;
                
                return;
            }

            if (!hit.collider.gameObject.GetInstanceID().Equals(hitGameObject))
            {
                if(hitGameObject != null && hitGameObject.GetComponent<Photographable>() != null)
                {
                    hitGameObject.SendMessage("HoverCameraExit");
                }

                hitGameObject = hit.collider.gameObject;

                if(hitGameObject.GetComponent<Photographable>() != null)
                {
                    hitGameObject.SendMessage("HoverCameraOver");
                }
            }

            else
            {
                return;
            }
        }

        else if(hitGameObject != null && hitGameObject.GetComponent<Photographable>() != null)
        {
            hitGameObject.SendMessage("HoverCameraExit");

            hitGameObject = null;
        }
    }
}
