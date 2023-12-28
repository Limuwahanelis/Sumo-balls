using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    [SerializeField] GameObject _currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPanel(GameObject panelToShow)
    {
        _currentPanel.SetActive(false);
        _currentPanel = panelToShow;
        _currentPanel.SetActive(true);
    }
}
