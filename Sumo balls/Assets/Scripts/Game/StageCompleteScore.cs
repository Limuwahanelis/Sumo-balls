using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class StageCompleteScore : MonoBehaviour
{
    /// <summary>
    /// Current score in range from 0 to 3
    /// </summary>
    public int Score => _score;
    /// <summary>
    /// Score as index where score = 0 or 1 means 0 index 
    /// </summary>
    public int ScoreAsIndex {
        get {
            if (_score == 0) return 0;
            return _score-1;
        }
    }
    /// <summary>
    /// Score as index where score = 0 or 1 means 2 index 
    /// </summary>
    public int ScoreAsReversedIndex
    {
        get
        {
            if (_score == 0) return 2;
            return 3-_score;
        }
    }
    [SerializeField] Color _completedColor;
    [SerializeField] Color _failedColor;
    [SerializeField] List<Image> _stars;
    [SerializeField] List<TMP_Text> _starsDescription;

    private int _score;
    public void ReduceScore()
    {
        _score--;
        _stars[_score].color = _failedColor;
        _starsDescription[_score].color = _failedColor;
    }
    public void IncreaseScore()
    {
        _score++;
        _stars[_score].color = _completedColor;
        _starsDescription[_score].color = _completedColor;
    }

    public void SetScore(int value)
    {
        value = math.clamp(value,0, 3);
        _score = value;
        for(int i=0;i<3;i++)
        {
            if(i<value)
            {
                _stars[i].color= _completedColor;
                _starsDescription[i].color = _completedColor;
            }
            else
            {
                _stars[i].color = _failedColor;
                _starsDescription[i].color = _failedColor;
            }
        }
    }
    public void SetDescription(List<string> descriptions)
    {
        for(int i=0;i<3;i++)
        {
            _starsDescription[i].text = descriptions[i];
        }
    }
}
