using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameModeManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] protected bool _debug;
    [SerializeField] protected GameModeSettings _debugSettings;
#endif
    [SerializeField] protected PauseSetter _gameOverPause;
    [SerializeField] protected PauseSetter _stageClearPause;
    [SerializeField] protected EnemySpawner _enemySpawner;
    [SerializeField] protected PowerUpSpawner _powerUpSpawner;
    public UnityEvent OnResetStage;
    public abstract void RestartStage();
}
