using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]bool _loadOnStart;
    [SceneName,SerializeField] string _sceneToLoad;
    [SerializeField] UnityEvent OnBeforeSceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        if (_loadOnStart)
        {
            OnBeforeSceneLoad?.Invoke();
            SceneManager.LoadScene(_sceneToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        OnBeforeSceneLoad?.Invoke();
        SceneManager.LoadScene(_sceneToLoad);
    }
    public void Load(string sceneName)
    {
        OnBeforeSceneLoad?.Invoke();
        SceneManager.LoadScene(sceneName);
    }
}
