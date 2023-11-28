using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleLocalization;
using UnityEngine.UI;
using TMPro;

public class MultiLanguage : MonoBehaviour
{
    // Chương trình quản lý, hiển thị ngôn ngữ trong game
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI changeLanguageText;
    public TextMeshProUGUI chooseDiffcultyText;
    public Text easyText;
    public Text mediumText;
    public Text hardText;
    public Text endlessText;
    public TextMeshProUGUI volumeText;
    public Text quitText;
    public Text resumeText;
    public Text menuText;
    public Text quitText2;
    public Text restartText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI endLessGuideText;
    public TextMeshProUGUI winText;
    public Text backToMenuText;
    public TextMeshProUGUI endlessGameOverText;
    public Text endlessRestartText;

    private void Awake()
    {
        LocalizationManager.Read();
        LocalizationManager.Language = "Japanese";
    }

    public void Language(string language)
    {
        //Màn hình bắt đầu
        LocalizationManager.Language = language;
        titleText.text = LocalizationManager.Localize("Message.Title");
        changeLanguageText.text = LocalizationManager.Localize("Message.ChangeLanguage");
        chooseDiffcultyText.text = LocalizationManager.Localize("Message.ChooseDiffculty");
        easyText.text = LocalizationManager.Localize("Message.Easy");
        mediumText.text = LocalizationManager.Localize("Message.Medium");
        hardText.text = LocalizationManager.Localize("Message.Hard");
        endlessText.text = LocalizationManager.Localize("Message.Endless");
        volumeText.text = LocalizationManager.Localize("Message.Volume");
        quitText.text = LocalizationManager.Localize("Message.Quit");
        //Trình đơn tạm dừng
        resumeText.text = LocalizationManager.Localize("Message.Resume");
        menuText.text = LocalizationManager.Localize("Message.Menu");
        quitText2.text = LocalizationManager.Localize("Message.Quit");
        //Văn bản hiển thị sau khi Game Over
        gameOverText.text = LocalizationManager.Localize("Message.GameOver");
        restartText.text = LocalizationManager.Localize("Message.Restart");
        //Hướng dẫn hiển thị ở chế độ vô tận
        endLessGuideText.text = LocalizationManager.Localize("Message.EndlessGuide");
        //Văn bản hiển thị sau chiến thắng
        winText.text = LocalizationManager.Localize("Message.Win");
        backToMenuText.text = LocalizationManager.Localize("Message.BackToMenu");
        //Văn bản xuất hiện sau khi trò chơi kết thúc ở chế độ vô tận
        endlessGameOverText.text = LocalizationManager.Localize("Message.EndlessGameOver");
        endlessRestartText.text = LocalizationManager.Localize("Message.EndlessRestart");

        if (LocalizationManager.Language == "English")
        {
            titleText.fontSize = 90;
        } else if (LocalizationManager.Language == "Japanese")
        {
            titleText.fontSize = 100;
        }
    }
}
