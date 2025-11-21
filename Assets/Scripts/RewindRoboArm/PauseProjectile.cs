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

                float AOEscale = 1f + 0.5f * charge;
                sphere.transform.localScale = Vector3.one * AOEscale;

                var freeze = AOEShpere.GetComponent<FreezeAOE>();
                if (freeze != null)
                {
                    freeze.duration = 0.6f * charge;
                }
            }
            Destroy(gameObject);
        }
    }
}
