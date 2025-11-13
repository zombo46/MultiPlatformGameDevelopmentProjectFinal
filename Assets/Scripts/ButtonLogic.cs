using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClicked()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        transform.parent.gameObject.SendMessage("ButtonClicked", gameObject);
    }
}
