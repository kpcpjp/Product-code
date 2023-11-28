using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private Timer timer;
    public static int numberRound;
    public static int startMinutes;
    public static int startSeconds;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Canvas").GetComponent<Timer>();
    }

    // Thiết lập thời gian và số vòng đua cho các chế độ khó khi người chơi chọn
    public void EasyMode()
    {
        if (numberRound == 1)
        {
            startMinutes = 1;
            startSeconds = 30;
        } else if (numberRound == 2)
        {
            startMinutes = 3;
            startSeconds = 0;
        } else if (numberRound == 3)
        {
            startMinutes = 4;
            startSeconds = 30;
        }
    }
    public void MediumMode()
    {
        if (numberRound == 1)
        {
            startMinutes = 1;
            startSeconds = 15;
        } else if (numberRound == 2)
        {
            startMinutes = 2;
            startSeconds = 30;
        } else if (numberRound == 3)
        {
            startMinutes = 3;
            startSeconds = 45;
        }
    }

    public void ChallengeMode()
    {
        if (numberRound == 1)
        {
            startMinutes = 1;
            startSeconds = 0;
        } else if (numberRound == 2)
        {
            startMinutes = 2;
            startSeconds = 0;
        } else if (numberRound == 3)
        {
            startMinutes = 3;
            startSeconds = 0;
        }
    }
}
