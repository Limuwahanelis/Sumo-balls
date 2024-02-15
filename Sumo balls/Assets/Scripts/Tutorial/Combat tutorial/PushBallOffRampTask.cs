using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBallOffRampTask : TutorialTask
{
    [SerializeField] NormalEnemy enemy;
    private void Awake()
    {
        enemy.OnDeath.AddListener(CompleteTask);
    }
    public override void StartTask()
    {
        base.StartTask();

    }
    private void CompleteTask(Enemy en)
    {
        enemy.OnDeath.RemoveListener(CompleteTask);
        OnTaskCompleted?.Invoke();
    }
}
