using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject startMenu;

    public void ButtonClicked(GameObject gameObject)
    {
        string buttonTag = gameObject.tag;
        switch(buttonTag)
        {
            case "BackButton":
                OnBackClicked();
                break;
        }
    }

    public void OnBackClicked()
    {
        gameObject.SetActive(false);
        startMenu.SetActive(true);
    }
}
