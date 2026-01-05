using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Photograph/PhotographObject")]

public class PhotographObject : ScriptableObject
{
    [SerializeField] private int photoNum;

    [SerializeField] [TextArea] private string photoDescription;

    [SerializeField] private Texture2D photo;

    [SerializeField] private string photoLable;

    public int PhotoNum => photoNum;

    public string PhotoDescription => photoDescription;

    public Texture2D Photo => photo;

    public string PhotoLable => photoLable;
}
