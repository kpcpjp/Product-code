using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 startPos;
    public Quaternion startRotation;
    private Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        // Thiết lập ban đầu khi chơi
        startPos = player.transform.position;
        startRotation = player.transform.rotation;
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over khi thời gian về 0
        if (timer.currentTime <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    /// <summary>
    /// Chương trình cho chức năng trở lại checkpoint
    /// </summary>
    public void ReturnCheckpoint()
    {
        player.transform.position = startPos;
        player.transform.rotation = startRotation;
    }
}
