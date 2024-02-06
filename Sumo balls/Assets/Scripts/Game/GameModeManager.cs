using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameModeManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] public bool debug;
    [SerializeField] protected GameModeSettings _debugSettings;
#endif
    [SerializeField] protected StageCompleteScore _stageCompleteScore;
    [SerializeField] protected InStageDescription _taskDescription;
    [SerializeField] protected PauseSetter _gameOverPause;
    [SerializeField] protected EnemySpawner _enemySpawner;
    [SerializeField] protected PowerUpSpawner _powerUpSpawner;
    public UnityEvent OnResetStage;
    public UnityEvent OnStageCompleted;
    public UnityEvent OnStageFailed;
    public UnityEvent OnStageStarted;
    public abstract void RestartStage();
    public abstract void PrepareStage();
    public abstract void StartStage();

    public abstract void FailStage();
}
