using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckOnEnableIfNextStageExists : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    public UnityEvent OnStageExists;
    public UnityEvent OnStageNotExists;
    private void OnEnable()
    {
        Exists();
    }
    public void Exists()
    {
        if (_stageList.stages.IndexOf(GlobalSettings.SelectedStage) == _stageList.stages.Count - 1) OnStageNotExists?.Invoke();
        OnStageExists?.Invoke();
    }
}
