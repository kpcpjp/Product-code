using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chương trình bảng điều khiển tạm dừng 
public class PauseMenu : MonoBehaviour
{
    //Thiết lập trạng thái tạm dừng mặc định là false
    public static bool GameIsPausesd = false;
    //Hiển thị PauseMenu trong Unity
    public GameObject pauseMenuUI;
    //Biến để lấy GameManager.cs
    private GameManager gameManager;

    void Start()
    {
        //Lấy GameManager.cs
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quản lý trạng thái tạm dừng
        if ((Input.GetKeyDown(KeyCode.Escape)) && (gameManager.isGameActive))
        {
            if(GameIsPausesd)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    //Phương thức nút tiếp theo
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausesd = false;
    }

    //Phương thức trạng thái tạm dừng
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausesd = true;
    }

    //Phương thức nút menu
    public void LoadMenu()
    {
        gameManager.RestartGame();
        Time.timeScale = 1f;
    }

    //Phương thức nút thoát
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

}
