using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class LogLogic : MonoBehaviour
{
    public LogObject logObject;

    public GameObject textBox;
    public void LogSetup(int index)
    {
        logObject = transform.parent.gameObject.GetComponent<LogLoader>().logObjects[index];

        textBox.GetComponent<TMP_Text>().text = logObject.Summary;

        Debug.Log("log object index: " + logObject.LogNum.ToString());
    }
}
