using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class RewindBeamShooter : MonoBehaviour
{

    public Camera mainCamera;
    public Transform firePoint;
    public GameObject beamPrefab;
    public float range = 1.7f;
    public LayerMask rewindable;
    public static bool isFiringBeam = false;

    private InputAction fireAction;
    private GameObject currentBeam;
    private VisualEffect visualEffect;
    private IRewindable RewindTarget;

    private Transform posA;
    private Transform posB;
    private Transform posC;
    private Transform posD;

    void OnEnable()
    {
        fireAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        fireAction.Enable();
    }

    void OnDisable()
    {
        fireAction.Disable();
    }
  
    void Update()
    {
        if (fireAction == null)
        {
            return;
        }
        if (fireAction.WasPressedThisFrame()) 
        {
            StartRBeam();
        }
        if (fireAction.IsPressed() && currentBeam != null)
        {
            updateRBeam();
        }
        if (fireAction.WasReleasedThisFrame())
        {
            stopRBeam();
        }
    }

    void StartRBeam()
    {
        isFiringBeam = true;
        currentBeam = Instantiate(beamPrefab);
        visualEffect = currentBeam.GetComponent<VisualEffect>();

        posA = currentBeam.transform.Find("PosA");
        posB = currentBeam.transform.Find("PosB");
        posC = currentBeam.transform.Find("PosC");
        posD = currentBeam.transform.Find("PosD");

        posA.position = firePoint.position;
        posB.position = firePoint.position; 
        posC.position = firePoint.position;
        posD.position = firePoint.position + firePoint.forward * range;

        updateRBeam();
    }

    void updateRBeam()
    {
       posA.position = firePoint.position;
       Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
       Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        { 
            if(((1 << hit.collider.gameObject.layer) & rewindable) != 0)
            {
                targetPoint = hit.point;
                posD.position = targetPoint;
                IRewindable rewindable = hit.collider.GetComponent<IRewindable>();
                if (rewindable != null && rewindable != RewindTarget)
                {
                    RewindTarget?.OnRewindBeamExit();
                    RewindTarget = rewindable;
                    RewindTarget.OnRewindBeamHit(targetPoint);
                }
            }
            else
            {
                if (RewindTarget != null) 
                {
                    RewindTarget?.OnRewindBeamExit();
                    RewindTarget = null;
                }
                targetPoint = firePoint.position + (ray.direction.normalized * range);
                Vector3 direction = (targetPoint - firePoint.position).normalized;
                posD.position = firePoint.position + direction * 1.5f;
            }

        }
        else
        {
            if (RewindTarget != null)
            {
                RewindTarget?.OnRewindBeamExit();
                RewindTarget = null;
            }
            targetPoint = firePoint.position + (ray.direction.normalized * range);
            Vector3 direction = (targetPoint - firePoint.position).normalized;
            posD.position = firePoint.position + direction * 1.5f;

        }
       
        posB.position = firePoint.position;
        posC.position = firePoint.position;
    }

    void stopRBeam()
    {
        if (currentBeam != null)
        {
            isFiringBeam = false;
            Destroy(currentBeam);
            currentBeam = null;
            visualEffect = null;
        }
    }
}
