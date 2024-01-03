using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadPlayerConfigs : MonoBehaviour
{
    [SerializeField] InputActionAsset _playerActions;
    [SerializeField] bool _loadOnAwake;

    private void Awake()
    {
        if (_loadOnAwake) LoadPlayerRebinds();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LoadPlayerRebinds()
    {
        string rebinds = PlayerPrefs.GetString(ConfigsPaths.controlRebindsPlayerPrefsName);
        if (!string.IsNullOrEmpty(rebinds))
            _playerActions.LoadBindingOverridesFromJson(rebinds);
    }
}
