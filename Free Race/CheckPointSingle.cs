using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Hệ thống checkpoint trong bản đồ
/// </summary>
public class CheckPointSingle : MonoBehaviour
{
    //public GameObject[] checkPointList;
    private Vector3 checkStartPos;
    private Quaternion checkStartRotation;
    private GameManager gameManager;
    public GameObject nextCheckpoint;
    public static int count;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        checkStartPos = transform.position;
        count = 1;
    }
    private void OnTriggerEnter(Collider orther)
    {   
        checkStartPos = orther.transform.position;
        checkStartRotation = orther.transform.rotation;
        gameManager.startPos = checkStartPos;
        gameManager.startRotation = checkStartRotation;
        gameObject.SetActive(false);
        nextCheckpoint.SetActive(true);
        Debug.Log("Checkpoint");
        Debug.Log(count);

        if (gameObject.tag == "WinCheckpoint")
        {
            if (count == DifficultyManager.numberRound)
            {
                SceneManager.LoadScene("Win Scene");
            }
            count+= 1;
        }
    }
}
