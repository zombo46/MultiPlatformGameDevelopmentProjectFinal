using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzlePlatform : MonoBehaviour
{
    [SerializeField] public PlatformType platformType;

    [SerializeField] private int platformNum;

    [SerializeField] private GameObject trigger;

    public static List<GameObject> platformsSteptOn = new List<GameObject>();

    void Start()
    {
        if(platformType == PlatformType.Number)
        {
            SetPlatformNum(platformNum);
        }
    }

    public void ToggleColider(bool state)
    {
        GetComponent<BoxCollider>().enabled = state;
    }

    private void ToggleTrigger(bool state)
    {
        trigger.GetComponent<BoxCollider>().enabled = state;
    }

    public void SetPlatformNum(int num)
    {
        GameObject[] numberGameObjects = transform.parent.GetComponent<LavaPuzzle>().numberGameObjects;

        GameObject number = Instantiate(numberGameObjects[num]);
        number.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 90.0f);
        number.transform.localScale.Scale(new Vector3(0.5f, 0.5f, 0.5f));
        number.transform.SetParent(transform);
        
        Debug.Log(number.transform.position);
        
        number.transform.localPosition = new Vector3(0.0f, 0.51f, 0.0f);
    }

    public void StepOnPlatform()
    {
        LightUpPlatform();

        platformsSteptOn.Add(gameObject);

        CheckList();
    }

    public void LeavePlatform()
    {
        ToggleColider(false);

        ToggleTrigger(false);
    }

    public void ResetPlatform()
    {
        DimPlatform();

        ToggleColider(true);

        ToggleTrigger(true);
    }

    private void LightUpPlatform()
    {
        GetComponent<Highlight>().ToggleHighlight(true);
    }

    private void DimPlatform()
    {
        GetComponent<Highlight>().ToggleHighlight(false);
    }

    private void CheckList()
    {
        bool success = true;
        for(int i = 0; i < platformsSteptOn.Count; i++)
        {
            switch (platformsSteptOn[i].GetComponent<PuzzlePlatform>().platformType)
            {
                case PlatformType.Pink:
                    if(platformsSteptOn.Count - i < 2)
                    {
                        break;
                    }

                    if(!(platformsSteptOn[i+1].GetComponent<PuzzlePlatform>().platformType == PlatformType.Blue) || !(platformsSteptOn[i+2].GetComponent<PuzzlePlatform>().platformType == PlatformType.Blue))
                    {
                        Debug.Log(platformsSteptOn[i+1]);
                        success = false;
                        Debug.Log("Pink fail.");
                    }

                    break;
                
                case PlatformType.Blue:
                    if(platformsSteptOn.Count - i < 4)
                    {
                        break;
                    }

                    if(platformsSteptOn[i+1].GetComponent<PuzzlePlatform>().platformType == PlatformType.Blue && platformsSteptOn[i+2].GetComponent<PuzzlePlatform>().platformType == PlatformType.Blue && !(platformsSteptOn[i+3].GetComponent<PuzzlePlatform>().platformType == PlatformType.Pink))
                    {
                        success = false;
                        Debug.Log("Blue fail.");
                    }

                    break;
                
                case PlatformType.Number:
                    if(i%10 != platformsSteptOn[i].GetComponent<PuzzlePlatform>().platformNum)
                    {
                        success = false;
                        Debug.Log(i);
                        Debug.Log(platformsSteptOn[i].GetComponent<PuzzlePlatform>().platformNum);
                        Debug.Log("Number fail.");
                    }

                    break;
            }

            if (!success)
            {
                Fail();
                break;
            }
        }

        if (success)
        {
            transform.parent.SendMessage("CheckWinCon", platformsSteptOn.Count);
        }
    }

    private void Fail()
    {
        gameObject.transform.parent.gameObject.SendMessage("Failed");
    }

    public enum PlatformType
    {
        Pink,
        Blue,
        Number
    }
}
