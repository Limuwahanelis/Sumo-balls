using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndScreen : MonoBehaviour
{
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _stageClearScreen;
    [SerializeField] CursorLockControl _lockControl;
    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        _gameOverScreen.SetActive(true);
        _stageClearScreen.SetActive(false);
        _lockControl.SetCursorVisibilty(true);
    }

    public void ShowStageClearScreen()
    {
        gameObject.SetActive(true);
        _gameOverScreen.SetActive(false);
        _stageClearScreen.SetActive(true);
        _lockControl.SetCursorVisibilty(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        _lockControl.SetCursorVisibilty(false);
    }
}
