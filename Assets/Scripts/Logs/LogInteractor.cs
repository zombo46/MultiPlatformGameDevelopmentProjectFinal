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

    [SerializeField] private LogObject testLog;

    public UnityEvent ePressed = new UnityEvent();

    public void Start()
    {
        ePressed.AddListener(OpenBox);
    }

    public void Interact(Collider collider)
    {
        Debug.Log("Invoking ePressed.");
        ePressed.Invoke();
    }

    public void OpenBox()
    {
        textBox.gameObject.GetComponent<LogUI>().testLogObject = testLog;
        Debug.Log("ePresseed Invoked.");

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
        ePressed.RemoveAllListeners();
        Debug.Log("ePresseed Invoked.");

        if (!textBox.activeInHierarchy)
        {
            return;
        }

        textBox.SetActive(false);
        player.GetComponent<PlayerMovement>().setMovable(true);

        ePressed.RemoveAllListeners();
        ePressed.AddListener(OpenBox);
    }
}
