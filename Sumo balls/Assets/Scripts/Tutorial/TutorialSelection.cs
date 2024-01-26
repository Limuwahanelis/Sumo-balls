using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelection : MonoBehaviour
{
    [SerializeField] GameObject _skipWindow;
    [SerializeField] LoadScene _tutorialLoadScene;
    [SerializeField] ShowHidePanel _showHidePanel;
    [SerializeField] BoolReference _skipTutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTutorial()
    {
        if(SaveGameData.GameData.isTutorialCompleted)
        {
            _showHidePanel.ShowPanel(gameObject);
        }
        else
        {
            _tutorialLoadScene.Load();
        }
    }

    public void SetSkipValue(bool value)
    {
        _skipTutorial.value = value;
    }

}
