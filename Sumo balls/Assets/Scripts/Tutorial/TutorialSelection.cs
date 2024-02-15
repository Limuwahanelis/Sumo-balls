using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelection : MonoBehaviour
{
    [SerializeField] GameObject _controlsSkipWindow;
    [SerializeField] GameObject _combatskipWindow;
    [SerializeField] LoadScene _controlsTutorialLoadScene;
    [SerializeField] LoadScene _combatTutorialLoadScene;
    [SerializeField] ShowHidePanel _showHidePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayControlsTutorial()
    {
        if(GameDataManager.GameData.isControlsTutorialCompleted)
        {
            _showHidePanel.ShowPanel(_controlsSkipWindow);
            gameObject.SetActive(true);
        }
        else
        {
            _controlsTutorialLoadScene.Load();
        }
    }
    public void PlayCombatTutorial()
    {
        if (GameDataManager.GameData.isCombatTutorialCompleted)
        {
            _showHidePanel.ShowPanel(_combatskipWindow);
            gameObject.SetActive(true);
        }
        else
        {
            _combatTutorialLoadScene.Load();
        }
    }

}

