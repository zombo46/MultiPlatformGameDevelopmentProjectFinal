using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectedPhoto : MonoBehaviour
{
    [SerializeField] private PhotographObject[] photographObjects;

    [SerializeField] private GameObject description;

    [SerializeField] private GameObject photo;

    [SerializeField] private GameObject photoLable;

    public void LoadPhoto(int photoNum)
    {
        gameObject.SetActive(true);
        PhotographObject photographObject = photographObjects[photoNum];

        description.GetComponent<TMP_Text>().text = photographObject.PhotoDescription;

        photo.GetComponent<Image>().sprite = Sprite.Create(photographObject.Photo, new Rect(0.0f, 0.0f, photographObject.Photo.width, photographObject.Photo.height), new Vector2(0.0f, 0.0f));
        photo.GetComponent<Image>().preserveAspect = true;

        photoLable.GetComponent<TMP_Text>().text = photographObject.PhotoLable;
    }
}
