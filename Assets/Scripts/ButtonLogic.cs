using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public void OnClicked()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        transform.parent.gameObject.SendMessage("ButtonClicked", gameObject);
    }
}
