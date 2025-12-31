using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LogLoader : MonoBehaviour
{
    public GameObject logPrefab;

    public GameObject playerMenu;

    public GameObject logContainer;

    public LogObject[] logObjects;

    void OnEnable()
    {
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

        string saveString = reader.ReadToEnd();

        reader.Close();

        Debug.Log(saveString);

        for(int i = 0; i < saveString.Length; i++)
        {
            DisplayLog(int.Parse(saveString[i].ToString()));
        }
    }

    void OnDisable()
    {
        foreach(Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    private void DisplayLog(int index)
    {
        Debug.Log(index);
        GameObject instance = Instantiate(logPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(transform);
        instance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        instance.SendMessage("LogSetup", index);
    }

    public void ButtonClicked(GameObject gameObject)
    {
        Debug.Log(gameObject.GetComponent<LogLogic>().logObject.LogNum.ToString());

        logContainer.GetComponent<LogUI>().testLogObject = gameObject.GetComponent<LogLogic>().logObject;

        //playerMenu.SendMessage("OnBackClicked");

        GetComponent<LogInteractor>().ePressed.Invoke();
    }
}
