using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.SimpleLocalization;

// Chương trình quản lý trò chơi
public class GameManager : MonoBehaviour
{
    // Danh sách các đối tượng sinh sản
    public List<GameObject> targets;
    //Văn bản điểm số
    public TextMeshProUGUI scoreText;
    // Văn bản số mạng
    public TextMeshProUGUI livesText;
    // Văn bản hướng dẫn
    public TextMeshProUGUI guideText;
    public TextMeshProUGUI endlessGuideText;

    // Văn bản khi Game Over
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI yourScoreText;

    // Nút Restart
    public Button restartButton;
    // Màn hình tiêu đề
    public GameObject titleScreen;
    // Văn bản hiển thị sau khi bắt đầu
    public GameObject onPlayScreen;
    // Màn hình hiện ra khi bạn thắng
    public GameObject winScreen;
    // Màn hình hiển thị khi GameOver trong chế độ vô tận
    public GameObject endlessGameoverScreen;
    // Cảm biến
    private GameObject sensor;
    // Tốc độ sinh sản
    public float spawnRate = 1.0f;
    // Điểm số
    public int score;
    // Số mạng
    [SerializeField] public int lives;
    // Biến để kiểm tra trạng thái chơi
    public bool isGameActive;
    public bool isEndlessModeActive;
    //Điểm điều kiện chiến thắng
    private int winScore;
    // Biến để lấy MultiLanguage.cs
    private MultiLanguage multiLanguage;

    private void Awake()
    {
        LocalizationManager.Read();
    }

    //Quản lý các đối tượng sinh sản
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    void Start()
    {
        //Tìm cảm biến trong hệ thống phân cấp và gán nó cho biến
        sensor = GameObject.Find("Sensor");
        // Lấy MultiLanguage.cs
        multiLanguage = GameObject.Find("Multi Language").GetComponent<MultiLanguage>();
        Application.targetFrameRate = 300;
    }

    /// <summary>
    /// Điều kiện chiến thắng và những gì hiển thị khi bạn giành chiến thắng
    /// </summary>
    public void Update()
    {
        if (score >= winScore && isGameActive)
            {
                // Màn hình chiến thắng sẽ được hiển thị
                winScreen.gameObject.SetActive(true);
                // Ngăn chặn các đối tượng sinh sản
                isGameActive = false;
                // Tắt cảm biến
                sensor.gameObject.SetActive(false);
            }  
    }

    /// <summary>
    /// Phương thức sau khi bắt đầu trò chơi trong các chế độ thường
    /// </summary>
    public void StartGame(float difficulty)
    {
        // Đặt trạng thái của chương trình CursorTrail.cs trong Main Camera thành true trong Hệ thống phân cấp
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
        // Hiển thị điểm số, số mạng và hướng dẫn
        onPlayScreen.gameObject.SetActive(true);
        guideText.gameObject.SetActive(true);
        // Ẩn màn hình bắt đầu
        titleScreen.gameObject.SetActive(false);
        // Đật isGameActive thành true
        isGameActive = true;
        // Đặt điểm thành 0
        score = 0;
        // Thực hiện Phương thức sinh sản
        StartCoroutine(SpawnTarget());
        //Thực hiện Phương thức tính điểm
        UpdateScore(0);
        // Tốc độ sinh sản thay đổi tùy theo độ khó
        spawnRate /= difficulty;
        // Tính và hiển thị số mạng
        UpdateLives(0);
        // Văn bản hiển thị tùy theo ngôn ngữ
        UpdateGuide(0);
    }

    /// <summary>
    /// Phương thức sau khi bắt đầu trò chơi trong chế độ vô tận
    /// </summary>
    public void StartGameEndless(float difficulty)
    {
        //Đặt trạng thái của chương trình CursorTrail.cs trong Main Camera thành true trong Hệ thống phân cấp
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
        // Hiển thị điểm số, số mạng và hướng dẫn
        onPlayScreen.gameObject.SetActive(true);
        endlessGuideText.gameObject.SetActive(true);
        // Ẩn màn hình bắt đầu
        titleScreen.gameObject.SetActive(false);
        // Đật isGameActive thành true
        isGameActive = true;
        isEndlessModeActive = true;
        // Đặt điểm thành 0
        score = 0;
        // Thực hiện Phương thức sinh sản
        StartCoroutine(SpawnTarget());
        //Thực hiện Phương thức tính điểm
        UpdateScore(0);
        //Tốc độ sinh sản thay đổi tùy theo độ khó
        spawnRate /= difficulty;
        // Tính và hiển thị số mạng
        UpdateLives(0);
        // Văn bản hiển thị tùy theo ngôn ngữ
        UpdateGuide(0);
    }

    /// <summary>
    /// Phương thức khi Game Over
    /// </summary>
    public void GameOver()
    {
        // Hiển thị Game Over 
        gameOverText.gameObject.SetActive(true);
        // Hiển thị nút Restart
        restartButton.gameObject.SetActive(true);
        //Đặt isGameActive thành false
        isGameActive = false;
    }

    /// <summary>
    /// Phương thức khi Game Over trong chế độ vô tận
    /// </summary>
    public void EndlessModeGameOver()
    {
        //Hiển thị màn hình Game Over
        endlessGameoverScreen.SetActive(true);
        //Đặt isGameActive và isEndlessModeActive thành false
        isGameActive = false;
        isEndlessModeActive = false;
    }

    //Phương thức nút thoát
    public void QuitGame()
    {
        Application.Quit();
    }

    //Phương pháp nút Restart
    public void RestartGame()
    {
        //Khởi động lại cảnh hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Điểm chiến thắng khác nhau tùy theo mức độ khó
    public void WinScreen(int setWinScore)
    {
        winScore = setWinScore;
    }

    //Phương thức tính điểm
    public void UpdateScore(int scoreToAdd)
    {
        //Lấy và tính điểm của đối tượng từ Target.cs
        score += scoreToAdd;

        //Cập nhật văn bản điểm theo ngôn ngữ
        if (LocalizationManager.Language == "English")
        {
            scoreText.text = "Score: " + score;
            yourScoreText.text = "Your Score: " + score; 
        } else if (LocalizationManager.Language == "Japanese")
        {
            scoreText.text = "スコア: " + score;
            yourScoreText.text = "あなたのスコア:" + score; 
        } 
    }

    //Phương thức tính số mạng
    public void UpdateLives(int subtractPoint)
    {
        // Giảm số mạng
        lives -= subtractPoint;
        // Hiển thị số mạng theo ngôn ngữ
        if (LocalizationManager.Language == "English")
        {
            livesText.text = "Lives: " + lives;
        } else if (LocalizationManager.Language == "Japanese")
        {
            livesText.text = "ライフ: " + lives;
        } 
    }

    // Hiển thị hướng dẫn theo ngôn ngữ
    public void UpdateGuide(int setWinScore)
    {
        if (LocalizationManager.Language == "English")
        {
            guideText.text = "Target: Try to get " + setWinScore + " score";
        } else if (LocalizationManager.Language == "Japanese")
        {
            guideText.text = "ターゲット: \n" + setWinScore + "ポイントをゲットしてください";
        } 
        
    }

}
