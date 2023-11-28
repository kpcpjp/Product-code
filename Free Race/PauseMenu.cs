using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Chương trình của màn hình tạm dừng (Pause Menu)
public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;
    public static bool GameIsPausesd = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quản lý trạng thái tạm dừng
        if (Input.GetKeyDown(KeyCode.Escape))
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

    //Chương trình cho nút tiếp tục
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausesd = false;
    }

    //Chương trình khi tạm dừng
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausesd = true;
    }

    //Chương trình cho nút Menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    //Chương trình cho nút thoát game
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

    //Chương trình cho nút trở lại checkpoint
    public void ReturnCheckpoint()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausesd = false;
        gameManager.ReturnCheckpoint();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
