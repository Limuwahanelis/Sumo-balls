using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectSelectableOnEnable : MonoBehaviour
{
    [SerializeField] Selectable _firstSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelect.gameObject);
    }
}
