using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuzzle : MonoBehaviour
{
    [SerializeField] public GameObject[] numberGameObjects;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject repairComponent;

    [SerializeField] private GameObject winPlatform;

    void Start()
    {
        player.GetComponent<PlayerVitality>().playerRevived.AddListener(ResetPuzzle);
    }

    public void CheckWinCon(int num)
    {
        if(num == GetComponentsInChildren<PuzzlePlatform>().Length)
        {
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("You Win!");

        repairComponent.SetActive(true);

        winPlatform.SetActive(true);
    }

    public void Failed()
    {
        Debug.Log("Failed");

        foreach(PuzzlePlatform puzzlePlatform in GetComponentsInChildren<PuzzlePlatform>())
        {
            puzzlePlatform.ToggleColider(false);
        }
    }

    public void ResetPuzzle()
    {
        PuzzlePlatform.platformsSteptOn.Clear();

        foreach(PuzzlePlatform puzzlePlatform in GetComponentsInChildren<PuzzlePlatform>())
        {
            puzzlePlatform.ResetPlatform();
        }
    }
}
