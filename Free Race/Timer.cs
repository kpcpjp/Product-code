using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// Bộ đếm thời gian
public class Timer : MonoBehaviour
{
    public bool timeActive;
    public int startMinutes;
    public int startSeconds;
    public float currentTime;

    [SerializeField] private TextMeshProUGUI currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        // Thiết lập số giây, số phút
        timeActive = true;
        // Thiết lập thời gian mặc định
        if ((DifficultyManager.startMinutes == 0) && (DifficultyManager.startSeconds == 0))
        {
            startMinutes = 1;
            startSeconds = 15;
            DifficultyManager.numberRound = 1;
        }
        // Thiết lập thời gian khi người chơi chọn chế độ
        else 
        {
            startMinutes = DifficultyManager.startMinutes;
             startSeconds = DifficultyManager.startSeconds;
        }
        currentTime = startMinutes * 60 + startSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    // Bộ đếm ngược và hiển thị thời gian
    public void UpdateTime()
    {
        if (timeActive == true)
        {
            currentTime = currentTime -Time.deltaTime;
            if (currentTime <= 0)
            {
                timeActive = false;
                Debug.Log("Time finished");
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = "タイム:\n " + time.Minutes.ToString() + ":" + 
                                    time.Seconds.ToString() + "s";
        }
    }
}
