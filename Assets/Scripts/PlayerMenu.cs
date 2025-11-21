using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public GameObject player;

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
        }
    }

    public void OnBackClicked()
    {
        gameObject.SetActive(false);
        player.SendMessage("OnPlayerMenuExit");
    }

    public void OnLogClicked()
    {
        
    }
}
