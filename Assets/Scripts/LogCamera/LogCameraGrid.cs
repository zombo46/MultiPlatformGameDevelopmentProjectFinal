using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCameraGrid : MonoBehaviour
{
    [SerializeField] private GameObject photoConainerPrefab;

    [SerializeField] private GameObject selectedPhotoTab;

    [SerializeField] public Texture2D[] images;
    void OnEnable()
    {
        foreach(int photoNum in Photographable.savedPhotos)
        {
            Debug.Log("Displaying photo no." + photoNum.ToString());
            DisplayPhoto(photoNum);
        }
    }

    void OnDisable()
    {
        foreach(Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    private void DisplayPhoto(int photoNum)
    {
        GameObject instance = Instantiate(photoConainerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(transform);
        instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        instance.SendMessage("PhotoSetup", photoNum);
    }

    public void ButtonClicked(GameObject button)
    {
        Debug.Log("Image no." + button.GetComponent<PhotoLogic>().photoNumber.ToString());

        gameObject.transform.parent.gameObject.SetActive(false);

        selectedPhotoTab.SetActive(true);

        selectedPhotoTab.SendMessage("LoadPhoto", button.GetComponent<PhotoLogic>().photoNumber);
    }
}
