using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTakenPopUp : MonoBehaviour
{
    [SerializeField] private GameObject logCamera;

    [SerializeField] private float waitTime = 2.0f;

    private LogCameraLogic logCameraLogic;
    // Start is called before the first frame update
    // void Start()
    // {
    //     logCameraLogic = logCamera.GetComponent<LogCameraLogic>();

    //     logCameraLogic.takePhoto.AddListener(Run);
    // }

    public void Run()
    {
        gameObject.SetActive(true);
        
        StartCoroutine(PopUp(waitTime));
    }

    IEnumerator PopUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }
}
