using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject logMenu;
    public GameObject logContainer;

    [SerializeField] GameObject photosContainer;

    [SerializeField] GameObject selectedPhotoTab;

    [SerializeField] GameObject shipContainer;

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("tab")))
        {
            OnBackClicked();
        }
    }

    public void ButtonClicked(GameObject gameObject)
    {
        string buttonTag = gameObject.tag;
        switch(buttonTag)
        {
            case "BackButton":
                OnBackClicked();
                break;
            
            case "LogButton":
                OnLogClicked();
                break;
            
            case "PhotosButton":
                OnPhotosClicked();
                break;
            case "ShipButton":
                OnShipClicked();
                break;
        }
    }

    public void OnPhotosClicked()
    {
        if (!photosContainer.activeInHierarchy)
        {
            if (logMenu.activeInHierarchy)
            {
                logMenu.SetActive(false);
            }

            if (selectedPhotoTab.activeInHierarchy)
            {
                selectedPhotoTab.SetActive(false);
            }

            photosContainer.SetActive(true);
            
            return;
        }

        photosContainer.SetActive(false);
    }

    public void OnBackClicked()
    {
        if (!logContainer.activeInHierarchy)
        {
            logMenu.SetActive(false);
            photosContainer.SetActive(false);
            selectedPhotoTab.SetActive(false);
            shipContainer.SetActive(false);
            gameObject.SetActive(false);
            player.SendMessage("OnPlayerMenuExit");
        }
    }

    public void OnLogClicked()
    {
        if (!logMenu.activeInHierarchy)
        {
            if (photosContainer.activeInHierarchy)
            {
                photosContainer.SetActive(false);
            }

            if (selectedPhotoTab.activeInHierarchy)
            {
                selectedPhotoTab.SetActive(false);
            }

            logMenu.SetActive(true);
            return;
        }
        logMenu.SetActive(false);
    }
    public void OnShipClicked()
    {
        if (!shipContainer.activeInHierarchy)
        {
            if (logMenu.activeInHierarchy) logMenu.SetActive(false);
            if (photosContainer.activeInHierarchy) photosContainer.SetActive(false);
            if (selectedPhotoTab.activeInHierarchy) selectedPhotoTab.SetActive(false);
            shipContainer.SetActive(true);
            return;
        }
        shipContainer.SetActive(false);
    }
}
