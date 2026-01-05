using UnityEngine;
using UnityEngine.InputSystem;

public class MapToggle_PlayerInput : MonoBehaviour
{
    [Header("UI")]
    public GameObject mapUI;

    [Header("Options")]
    public bool pauseOnOpen = true;
    public bool unlockCursorOnOpen = true;

    bool mapOpen = false;
    float storedTimeScale = 1f;

    void Start()
    {
        if (mapUI) mapUI.SetActive(false);
    }

    public void OnToggleMap(InputAction.CallbackContext context)
    {
        if (context.performed)
            ToggleMap();
    }

    public void ToggleMap()
    {
        mapOpen = !mapOpen;
        if (mapUI) mapUI.SetActive(mapOpen);

        if (pauseOnOpen)
        {
            if (mapOpen)
            {
                storedTimeScale = Time.timeScale;
                Time.timeScale = 0f; // pause
            }
            else
            {
                Time.timeScale = storedTimeScale;
            }
        }

        if (unlockCursorOnOpen)
        {
            Cursor.lockState = mapOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = mapOpen;
        }
    }
}
