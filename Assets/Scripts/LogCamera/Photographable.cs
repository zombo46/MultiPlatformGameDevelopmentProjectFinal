using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Photographable : MonoBehaviour
{
    public static List<int> savedPhotos = new List<int>();

    public UnityEvent photoTaken = new UnityEvent();

    [SerializeField]public GameObject logCamera;

    [SerializeField] public GameObject logCameraUI;

    private LogCameraLogic logCameraLogic;

    [SerializeField] private int photoNum;

    private bool isInFrame = false;

    void Start()
    {
        logCameraLogic = logCamera.GetComponent<LogCameraLogic>();

        photoTaken.AddListener(logCameraUI.transform.GetChild(1).GetComponent<PhotoTakenPopUp>().Run);
    }

    public void TakePhoto()
    {
        if (!savedPhotos.Contains(photoNum))
        {
            savedPhotos.Add(photoNum);
            
            Debug.Log("Taken Photo no." + photoNum + ". List now: " + savedPhotos.ToString());

            photoTaken.Invoke();

            string path = Path.Combine(Application.persistentDataPath, "savedPhotos.txt");

            if (!Directory.Exists(Application.persistentDataPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath);
            }
        

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            StreamWriter writer = new StreamWriter(path, true);

            writer.Write(savedPhotos[savedPhotos.Count - 1]);

            writer.Close();
        }
    }

    public void HoverCameraOver()
    {
        //Debug.Log("Hovering over object: " + gameObject.name);
        isInFrame = true;
        logCameraLogic.takePhoto.AddListener(TakePhoto);
        //make object light up
        gameObject.GetComponent<Highlight>().ToggleHighlight(true);
    }

    public void HoverCameraExit()
    {
        //Debug.Log("Exited hovering over object: " + gameObject.name);
        isInFrame = false;
        logCameraLogic.takePhoto.RemoveListener(TakePhoto);
        gameObject.GetComponent<Highlight>().ToggleHighlight(false);
    }
}
