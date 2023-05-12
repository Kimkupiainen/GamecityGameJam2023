using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public Button _Game;
    public Button _GameTwo;
    public Button _Quit;
    void Start()
    {

        _Game.onClick.AddListener(Game);
        _GameTwo.onClick.AddListener(GameTwo);
        _Quit.onClick.AddListener(QuitGame);
    }

    public void Game()
    {
        SceneManager.LoadScene(1);
    }

    public void GameTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
