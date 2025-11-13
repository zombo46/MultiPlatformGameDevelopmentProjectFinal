using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject OptionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicked(GameObject gameObject)
    {
        string buttonTag = gameObject.tag;
        switch(buttonTag)
        {
            case "StartButton":
                OnStartClicked();
                break;

            case "OptionsButton":
                OnOptionsClicked();
                break;
        }
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene(sceneName: "prototypeLevel");
    }

    public void OnOptionsClicked()
    {
        gameObject.SetActive(false);
        OptionsCanvas.SetActive(true);
    }
}
