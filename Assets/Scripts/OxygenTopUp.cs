using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OxygenTopUp : MonoBehaviour, IInteractable
{
    
    private float cooldownTime = 15f;
    private bool isOnCooldown = false;
    public GameObject Player;

    public void Interact(Collider other)
    {
        if (!isOnCooldown)
        {
            StartCoroutine(TopUpOxygen());
        }
    }
    private IEnumerator TopUpOxygen() 
    {
        float newOxygen = Random.Range(5f,16f);
        isOnCooldown = true;
        PlayerVitality vitality = Player.GetComponent<PlayerVitality>();
        if (vitality != null)
        {
            vitality.currentOxygen = vitality.currentOxygen + newOxygen;
            if (vitality.currentOxygen > vitality.maxOxygen)
            {
                vitality.currentOxygen = vitality.maxOxygen;
            }
            vitality.SendMessage("updateUI", SendMessageOptions.DontRequireReceiver);
        }
        var renderer = GetComponent<Renderer>();
        var color = renderer.material.GetColor("_BaseColor");
        renderer.material.SetColor("_EmissionColor", color * (-1f));
        var cooldown = cooldownTime;
        while (cooldown > 0)
        {
            renderer.material.SetColor("_EmissionColor", color * -(((cooldown / cooldownTime) * 20) - 10));
            cooldown -= Time.deltaTime;
            yield return null;
        }
        isOnCooldown = false;

        
    }
        
}

