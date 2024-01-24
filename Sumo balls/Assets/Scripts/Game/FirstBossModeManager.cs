using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FirstBossModeManager : GameModeManager
{
    private SpecialGameModeSettings _settings;
    [SerializeField] InStageDescription _timer;
    [SerializeField] Transform _BossSpawnTran;
    [SerializeField] FirstBoss _boss;
    [SerializeField] WallsManager _wallsManager;
    [SerializeField] List<float> _timeLimits = new List<float>() { 30, 45, 60 };
    private float _time;
    private bool _isCompleted;
    private bool _countTime;
    private int _timeLimitIndex;
    private void Awake()
    {
        _wallsManager.SetUp();
#if UNITY_EDITOR
        if (debug) _settings = _debugSettings as SpecialGameModeSettings;
        else _settings = GlobalSettings.SelectedStage.GameModeSettings as SpecialGameModeSettings;
        PrepareStage();
        return;
#endif
        _settings = GlobalSettings.SelectedStage.GameModeSettings as SpecialGameModeSettings;
        PrepareStage();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused || _isCompleted) return;
        if(_countTime)
        {
            _timer.SetValue(ConvertTime(_time));
            _time += Time.deltaTime;
        }
        if (_time> _timeLimits[_timeLimitIndex])
        {
            _stageCompleteScore.ReduceScore();
            _timeLimitIndex++;
        }

    }
    public override void PrepareStage()
    {
        _stageCompleteScore.SetScore(3);
        _stageCompleteScore.SetDescription(_settings.GetStarsDescription());
        _isCompleted = false;
        _boss.transform.position = _BossSpawnTran.position;
        _boss.enabled = false;
        _countTime = false;
        _time = 0;
        _taskDescription.SetValue("1");
        _timer.SetValue("0");
        _timeLimitIndex = 0;
    }

    public override void RestartStage()
    {
        OnResetStage?.Invoke();
        PrepareStage();
        
    }
    public void CompleteStage()
    {
        _isCompleted = true;
        OnStageCompleted?.Invoke();

    }
    private string ConvertTime(float timeInSeconds)
    {
        int miliSeconds = (int)((timeInSeconds - math.floor(timeInSeconds)) * 1000);
        int seconds = (int)math.floor(timeInSeconds);
        return string.Format("{0:00}:{1:000}", seconds, miliSeconds);
    }
    public void SetCountTime(bool value)
    {
        _countTime = value;
    }
    public void FailStage()
    {
        OnStageFailed?.Invoke();
    }
}
