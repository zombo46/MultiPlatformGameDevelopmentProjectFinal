using System.Collections;
using UnityEngine;
using TMPro;

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

        if (!PlayerPrefs.GetString("PlayerProgress").Contains(logObject.LogNum.ToString()))
        {
            PlayerPrefs.SetString("PlayerProgress", PlayerPrefs.GetString("PlayerProgress") + logObject.LogNum.ToString());
        }

        Debug.Log(PlayerPrefs.GetString("PlayerProgress"));
    }
}
