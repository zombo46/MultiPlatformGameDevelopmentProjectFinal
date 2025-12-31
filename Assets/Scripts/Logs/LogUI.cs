using System.Collections;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEditor;

public class LogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;
    
    public LogObject testLogObject;

    private GameObject atachedGameObject;

    private TypingEffect typingEffect;

    private bool proceed;

    private void OnEnable()
    {
        typingEffect = GetComponent<TypingEffect>();
        ShowLog(testLogObject);
    }

    public void ShowLog(LogObject logObject)
    {
        StartCoroutine(StepThroughLog(logObject));
    }

    public void Proceed(GameObject gameObject)
    {
        proceed = true;
        atachedGameObject = gameObject;
    }

    private IEnumerator StepThroughLog(LogObject logObject)
    {
        foreach(string log in logObject.Log)
        {
            yield return typingEffect.Run(log, textObject);
            proceed = false;
            yield return new WaitUntil(() => proceed == true);
        }

        atachedGameObject.SendMessage("CloseBox");

        SaveLog(logObject.LogNum);
    }

    private void SaveLog(int logIndex)
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

        //Read the text from directly from the test.txt file

        StreamReader reader = new StreamReader(path);

        string saveString = reader.ReadToEnd();

        reader.Close();

        //Write some text to the test.txt file

        if(!saveString.Contains(logIndex.ToString()))
        {
            StreamWriter writer = new StreamWriter(path, true);

            writer.Write(logIndex.ToString());

            writer.Close();
        }
    }
}
