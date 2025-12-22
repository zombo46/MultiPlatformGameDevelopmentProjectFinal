using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private float TypeSpeed = 50f;

    public Coroutine Run(string text, TMP_Text textObject)
    {
        return StartCoroutine(TypeText(text, textObject));
    }

    private IEnumerator TypeText(string text, TMP_Text textObject)
    {
        textObject.text = string.Empty;

        float time = 0;
        int index = 0;

        while(index < text.Length)
        {
            time += Time.deltaTime * TypeSpeed;
            index = Mathf.FloorToInt(time);
            index = Mathf.Clamp(index, 0, text.Length);

            textObject.text = text.Substring(0, index);

            yield return null;
        }

        textObject.text = text;
    }
}
