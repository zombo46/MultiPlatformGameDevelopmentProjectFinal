using UnityEngine;

[CreateAssetMenu(menuName = "Log/LogObject")]

public class LogObject : ScriptableObject
{
    [SerializeField] private int logNum;
    [SerializeField] [TextArea] private string[] log;

    public int LogNum => logNum;
    public string[] Log => log;
}
