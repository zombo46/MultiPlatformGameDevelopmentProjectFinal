using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogCameraLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "savedPhotos.txt");

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
            Photographable.savedPhotos.Add(int.Parse(saveString[i].ToString()));
        }
    }
}
