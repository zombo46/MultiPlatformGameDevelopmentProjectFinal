using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLogic : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerProgress");
    }
}
