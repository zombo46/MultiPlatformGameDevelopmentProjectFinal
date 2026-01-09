using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;}
    public PlayerInputActions inputActions;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInputActions();
        inputActions.Enable();

        LoadRebinds();
    }

    public void LoadRebinds()
    {
        foreach (var action in inputActions)
        {
            if (PlayerPrefs.HasKey(action.name))
            {
                action.LoadBindingOverridesFromJson(PlayerPrefs.GetString(action.name));
            }
        }
    }

    public void ResetAllBindings()
    {
        foreach (var action in inputActions)
        {
            action.RemoveAllBindingOverrides();
            PlayerPrefs.DeleteKey(action.name);
        }

        PlayerPrefs.Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
