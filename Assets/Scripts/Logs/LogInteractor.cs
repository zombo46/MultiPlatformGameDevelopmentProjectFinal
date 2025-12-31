using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LogInteractor : MonoBehaviour, IInteractable
{
    [SerializeField]
    public GameObject textBox;
    [SerializeField]
    public GameObject player;

    public GameObject playerMenu;

    [SerializeField] private LogObject testLog;

    public UnityEvent ePressed = new UnityEvent();
    
    private bool isLogOpen = false;

    public void Start()
    {
        ePressed.AddListener(OpenBox);
    }

    public void Interact(Collider collider)
    {
        if(!isLogOpen)
        {
            Debug.Log("Invoking ePressed.");
            ePressed.Invoke();
        }
    }

    void Update()
    {
        //Debug.Log("isLogOpen: " + isLogOpen + ", e pressed: " + Keyboard.current.eKey.wasPressedThisFrame);
        if(isLogOpen && Keyboard.current.eKey.wasPressedThisFrame)
        {
            ePressed.Invoke();
        }
    }

    public void OpenBox()
    {
        Debug.Log("Open Box");
        isLogOpen = true;
        if(testLog != null)
        {
            textBox.gameObject.GetComponent<LogUI>().testLogObject = testLog;
        }
        
        Debug.Log("ePresseed Invoked.");
        Debug.Log("isLogOpen: " + isLogOpen);

        if (textBox.activeInHierarchy)
        {
            return;
        }

        player.GetComponent<PlayerMovement>().setMovable(false);
        textBox.SetActive(true);

        ePressed.RemoveAllListeners();
        ePressed.AddListener(NextBox);
    }

    public void NextBox()
    {
        Debug.Log("next box");
        textBox.SendMessage("Proceed", gameObject);
    }

    public void CloseBox()
    {
        isLogOpen = false;
        ePressed.RemoveAllListeners();
        Debug.Log("close box");

        if (!textBox.activeInHierarchy)
        {
            return;
        }

        textBox.SetActive(false);
        if (!playerMenu.activeInHierarchy)
        {
            player.GetComponent<PlayerMovement>().setMovable(true);
        }

        ePressed.RemoveAllListeners();
        ePressed.AddListener(OpenBox);
    }
}
