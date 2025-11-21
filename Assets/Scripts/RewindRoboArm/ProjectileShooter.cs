using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileShooter : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject projectilePrefab;
    public Transform FirePoint;
    public float projectileSpeed = 20f;

    public float maxChargeTime = 1.5f;
    public float fullChargeCooldown = 1f;
    public float partialChargeCooldown = 1.5f;

    private Vector3 destination;
    private InputAction shootAction;

    private bool isCharging = false;
    private float currentChargeTime = 0f;
    private bool isOnCooldown = false;

    void OnEnable()
    {
        shootAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton");
        shootAction.Enable();
    }

    void OnDisable()
    {
        shootAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnCooldown) {
            return;
        }

        if (shootAction.IsPressed())
        {
            if (!isCharging) { 
                isCharging = true;
                currentChargeTime = 0f;
            }
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime > maxChargeTime) { 
                currentChargeTime = maxChargeTime;
                isCharging = false;
                StartCoroutine(shoot(currentChargeTime, fullChargeCooldown));
            }
        }
        else if (isCharging)
        {
            isCharging = false;
            float cooldownTime = (currentChargeTime >= maxChargeTime) ? fullChargeCooldown : partialChargeCooldown;
            StartCoroutine(shoot(currentChargeTime, cooldownTime));
        }
    }

    IEnumerator shoot(float charge, float cooldown) { 
        isOnCooldown = true;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        if (Physics.Raycast(ray, out RaycastHit hit)) { 
            destination = hit.point;
        }
        else { 
            destination = ray.GetPoint(1000);
        }

        GameObject projectile = Instantiate(projectilePrefab, FirePoint.position, Quaternion.identity);

        float scale = 1f + 3f * (charge / maxChargeTime);
        ParticleSystem ps = projectile.GetComponentInChildren<ParticleSystem>();
        if (ps != null) { 
            var main = ps.main;
            main.startSize = main.startSize.constant * scale;
        }

        projectile.transform.localScale = Vector3.one * scale;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (destination - FirePoint.position).normalized * projectileSpeed;

        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }

}
