using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    private InputAction action;
    public int bindingIndex;
    public TMP_Text bindingText;
    public string actionName;

    void Start()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager not found in the scene!");
            return;
        }

        action = InputManager.Instance.inputActions.FindAction(actionName);

        if (action == null)
        {
            Debug.LogError($"Action '{actionName}' not found! Check your PlayerInputActions asset.");
            return;
        }

        // Load saved binding if exists
        if (PlayerPrefs.HasKey(actionName))
        {
            string json = PlayerPrefs.GetString(actionName);
            action.LoadBindingOverridesFromJson(json);
        }

        UpdateText();
    }

    public void StartRebind()
    {
        if (action == null) return;

        bindingText.text = "Press a key...";
        action.Disable();

        action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // optional
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();

                SaveBinding();
                UpdateText();
            })
            .Start();
    }

    void UpdateText()
    {
        if (action == null)
        {
            bindingText.text = "No action!";
            return;
        }

        bindingText.text = action.GetBindingDisplayString(bindingIndex);
    }

    void SaveBinding()
    {
        if (action == null) return;

        PlayerPrefs.SetString(actionName, action.SaveBindingOverridesAsJson());
        PlayerPrefs.Save();
    }
}
