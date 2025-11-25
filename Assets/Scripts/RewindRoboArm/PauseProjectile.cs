using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseProjectile : MonoBehaviour
{
    public GameObject AOEShpere;
    public float charge = 0f;

    private bool collided = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!collided && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player")
        {
            collided = true;
            if (AOEShpere != null) { 
                GameObject sphere = Instantiate(AOEShpere, transform.position, Quaternion.identity);

                float AOEscale = 1.5f + 0.5f * charge;
                sphere.transform.localScale = Vector3.one * AOEscale;

                var blastDur = 1.8f + (2.5f - 1.8f) * charge;

                var freeze = sphere.GetComponent<FreezeAOE>();
                if (freeze != null)
                {
                    freeze.duration = blastDur;
                }
                Destroy(sphere, blastDur);
            }
            Destroy(gameObject);
        }
    }
}
