using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EndingCheck : MonoBehaviour
{
    [SerializeField] GameObject secondCamera;

    [SerializeField] GameObject spaceshipInteracable;

    [SerializeField] private int numPhotos;

    [SerializeField] private int numLogs;

    private string saveString;

    void Start()
    {
        spaceshipInteracable.GetComponent<SpaceshipInteractable>().onWinGame.AddListener(EndGame);
    }

    private void EndGame()
    {
        // secondCamera.SetActive(true);
        // secondCamera.GetComponent<Camera>().enabled = true;

        string path = Path.Combine(Application.persistentDataPath, "savedLogs.txt");

        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        

        if (!File.Exists(path))
        {
            File.Create(path);
        }

        StreamReader reader = new StreamReader(path);

        saveString = reader.ReadToEnd();

        reader.Close();

        if(Photographable.savedPhotos.Count >= numPhotos && saveString.Length >= numLogs)
        {
            GoodEnding();
        }

        else
        {
            BadEnding();
        }
    }

    private void GoodEnding()
    {
        GetComponent<SwitchCamera>().ShowGoodEnding();
    }

    private void BadEnding()
    {
        GetComponent<SwitchCamera>().ShowBadEnding();
    }
}
