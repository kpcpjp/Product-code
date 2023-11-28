using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Chương trình cho nút độ khó vô tận
/// </summary>
public class EndlessDiffcultyButton : MonoBehaviour
{
     // Biến cho nút
    private Button button;
    // Biến để lấy Gamemanger.cs
    private GameManager gameManager;
    //Biến để thiết lập độ khó trong Inspector của Unity
    public float difficulty;
    public int setWinScore;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        //Thêm một phương thức để chạy khi nhấn nút
        button.onClick.AddListener(SetDifficulty);
        button.onClick.AddListener(SetWinGame);
        // Lấy GameManager.cs
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

        void SetDifficulty()
    {
        //Thực thi phương thức StartGameEndless của gameManager với độ khó chế độ vô tận 
        gameManager.StartGameEndless(difficulty);
    }

        void SetWinGame()
    {
        //Sử dụng setWinScore để chạy phương thức WinScreen của gameManager
        gameManager.WinScreen(setWinScore);
    }
}
