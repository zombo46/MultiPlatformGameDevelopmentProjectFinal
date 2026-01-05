using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void OnEnable()
    {
        player.GetComponent<ProjectileShooter>().EnableShootAction();
    }

    void OnDisable()
    {
        player.GetComponent<ProjectileShooter>().DisableShootAction();
    }
}
