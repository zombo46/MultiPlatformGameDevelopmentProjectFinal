using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PhotoLogic : MonoBehaviour
{
    [SerializeField] private GameObject imageGameObject;

    public int photoNumber;

    private LogCameraGrid photoGrid;

    public void PhotoSetup(int photoNum)
    {
        photoNumber = photoNum;
        photoGrid = gameObject.transform.parent.GetComponent<LogCameraGrid>();

        imageGameObject.GetComponent<Image>().sprite = Sprite.Create(photoGrid.images[photoNum], new Rect(0.0f, 0.0f, photoGrid.images[photoNum].width, photoGrid.images[photoNum].height), new Vector2(0, 0));
        imageGameObject.GetComponent<Image>().preserveAspect = true;
    }
}
