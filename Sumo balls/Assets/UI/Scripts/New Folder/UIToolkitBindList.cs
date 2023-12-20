using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIToolkitBindList : MonoBehaviour
{
    [SerializeField] UIDocument uiDocument;

    [SerializeField] List<VisualElement> settingsOption = new List<VisualElement>();
    //private void OnEnable()
    //{
    //    uiDocument = GetComponent<UIDocument>();
    //    settingsOption = uiDocument.rootVisualElement.Query("Rebind").ToList();

    //    foreach (VisualElement element in settingsOption)
    //    {
    //        GameObject aa = new GameObject();
    //        aa.transform.parent = this.transform;
    //        aa.AddComponent<UIToolkitRebind>();
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        
    }
}
