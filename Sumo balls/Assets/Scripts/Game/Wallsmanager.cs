using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    [SerializeField] List<Wall> _walls = new List<Wall>();
    [SerializeField] StageCompleteScore _stageCompleteScore;
    [SerializeField] GameObject _cage;
    private int _collapsedWalls;
    public void SetUp()
    {
        _cage.SetActive(true);
        foreach (Wall wall in _walls)
        {
            wall.SetUp();
        }
    }
    public void SetUp(NormalModeSettings normalModeSettings)
    {
        
        _cage.SetActive(true);
        foreach (Wall wall in _walls)
        {
            wall.SetUp();
            wall.OnWallCollapsed.AddListener(() =>
            {
                Debug.Log("collapsed");
                _collapsedWalls++;
                Debug.Log(_stageCompleteScore.ScoreAsIndex + " "+ normalModeSettings.WallsRequiredForStar[_stageCompleteScore.ScoreAsIndex]);
                if (_collapsedWalls == normalModeSettings.WallsRequiredForStar[_stageCompleteScore.ScoreAsIndex])
                {
                    _stageCompleteScore.ReduceScore();
                }
            });
        }
    }
    //private void Start()
    //{
    //    foreach (Wall wall in _walls)
    //    {
    //        wall.Restore();
    //    }
    //}

    public void RestoreWalls()
    {
        _collapsedWalls = 0;
        foreach (Wall wall in _walls)
        {
            wall.Restore();
        }
    }
    private void OnDestroy()
    {
        foreach (Wall wall in _walls)
        {
            wall.OnWallCollapsed.RemoveAllListeners();
        }
    }
}
