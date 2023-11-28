using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Hệ thống bật tắt trạng thái hiển thị chữ và khung cảnh game
    public GameObject guideScreen;
    public GameObject gameStartScreen;
    public GameObject chooseModeScreen;
    public GameObject chooseNumberRound;
    public GameObject oneRoundMode;
    public GameObject twoRoundMode;
    public GameObject threeRoundMode;

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenGuide()
    {
        guideScreen.SetActive(true);
        gameStartScreen.SetActive(false);
    }

    public void CloseGuide()
    {
        guideScreen.SetActive(false);
        gameStartScreen.SetActive(true);
    }

    public void OpenChooseMode()
    {
        chooseModeScreen.SetActive(true);
        gameStartScreen.SetActive(false);
    }

    public void CloseChooseMode()
    {
        chooseModeScreen.SetActive(false);
        gameStartScreen.SetActive(true);
    }

    public void ShowOneRoundInfo()
    {
        chooseNumberRound.SetActive(false);
        oneRoundMode.SetActive(true);
        DifficultyManager.numberRound = 1;
    }

    public void ShowTwoRoundInfo()
    {
        chooseNumberRound.SetActive(false);
        twoRoundMode.SetActive(true);
        DifficultyManager.numberRound = 2;
    }

    public void ShowThreeRoundInfo()
    {
        chooseNumberRound.SetActive(false);
        threeRoundMode.SetActive(true);
        DifficultyManager.numberRound = 3;
    }

    public void BackToChooseNumberRound()
    {
        chooseNumberRound.SetActive(true);
        oneRoundMode.SetActive(false);
        twoRoundMode.SetActive(false);
        threeRoundMode.SetActive(false);
    }
}
