using UnityEngine;
using TMPro;

public class LogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject;

    private void OnEnable()
    {
        GetComponent<TypingEffect>().Run("Oh look at me\nI'm a cube oh my god!", textObject);
    }
}
