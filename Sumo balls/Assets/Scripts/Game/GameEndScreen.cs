using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndScreen : MonoBehaviour
{
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _stageClearScreen;
    [SerializeField] GameObject _canvas;
    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        _gameOverScreen.SetActive(true);
        _stageClearScreen.SetActive(false);
    }

    public void ShowStageClearScreen()
    {
        gameObject.SetActive(true);
        _gameOverScreen.SetActive(false);
        _stageClearScreen.SetActive(true);
    }
    public void Hide()
    {
        _canvas.SetActive(false);
        gameObject.SetActive(false);
    }
}
