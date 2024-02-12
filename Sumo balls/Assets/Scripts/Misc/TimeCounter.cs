using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public string FormattedTime { get { return ConvertTime(_time); } }
    public float CurrentTime => _time;
    private bool _countTime;
    private float _time=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_countTime) _time += Time.deltaTime;
    }

    public void SetCountTime(bool value)
    {
        _countTime = value;
    }
    public void ResetTimer()
    {
        _time = 0;
    }

    private string ConvertTime(float timeInSeconds)
    {
        int miliSeconds = (int)((timeInSeconds - math.floor(timeInSeconds)) * 1000);
        int seconds = (int)math.floor(timeInSeconds);
        return string.Format("{0:00}:{1:000}", seconds, miliSeconds);
    }
}
