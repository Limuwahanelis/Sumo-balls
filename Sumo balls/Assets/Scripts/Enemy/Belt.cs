using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] BoolReference _showBelt;
    [SerializeField] GameObject _belt;
    private void Awake()
    {
        _belt.SetActive(_showBelt.value);
        _showBelt?.variable.OnValueChanged.AddListener(SetBelt);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetBelt(bool value)
    {
        _belt.SetActive(value);
    }
    private void OnDestroy()
    {
        _showBelt?.variable.OnValueChanged.RemoveListener(SetBelt);
    }
}
