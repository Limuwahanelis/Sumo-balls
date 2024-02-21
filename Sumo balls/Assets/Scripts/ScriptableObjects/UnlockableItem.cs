using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName ="Unlockable item")]
[Serializable]
public class UnlockableItem : ScriptableObject
{

    public string Id { get { return _id; } private set { _id = value; } }
    [SerializeField] private string _id;


    public bool StartUnlocked => _startUnlocked;
    [SerializeField] bool _startUnlocked=false;

    public int Cost => _cost;
    [SerializeField] int _cost;

    public bool IsInitalised { get { return _isInitalised; } private set { _isInitalised = value; } }
    [SerializeField] private bool _isInitalised;
    private void Reset()
    {
        Init();
    }
    private void Init()
    {
        Debug.Log("in");

#if UNITY_EDITOR

        if (!_isInitalised)
        {
            Id = GUID.Generate().ToString();
            _isInitalised = true;
        }
#endif
        if (!_isInitalised)
        {
            Debug.LogError("Item was not initialised !");
        }
    }
}
