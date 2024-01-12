using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameModeDebugger : MonoBehaviour
{
    [SerializeField] GameModeManager _manager;
    [SerializeField] PlayerInputHandler _playerInputHandler;

    [SerializeField] bool _useDebugGameMode;

    private void Awake()
    {
        _manager = FindObjectOfType<GameModeManager>();
    }

    private void OnValidate()
    {
        if(_manager != null)
        {
#if UNITY_EDITOR
            _manager.debug = _useDebugGameMode;
#endif
        }
    }
}
