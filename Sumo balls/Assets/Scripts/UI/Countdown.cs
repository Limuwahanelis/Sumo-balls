using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
//#if UNITY_EDITOR
//    public bool skipCountdown;
//#endif
    [SerializeField] BoolReference _isStageFastLoad;
    [SerializeField] TMP_Text _countdownText;
    [SerializeField] float _timeForNumberToDisappear=1f;
    [SerializeField] int _startingNumber = 3;
    private float _startingFontSize;
    private float _time;
    private int _currentNumber;

    public UnityEvent OnCountdownEnd;
    public UnityEvent OnBeforeCountdown;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    public void Setup()
    {
        if (_isStageFastLoad.value)
        {
            OnBeforeCountdown?.Invoke();
            OnCountdownEnd?.Invoke();
            gameObject.SetActive(false);
        }
        else
        {
            OnBeforeCountdown?.Invoke();
            gameObject.SetActive(true);
            _countdownText.gameObject.SetActive(true);
            _currentNumber = _startingNumber;
            _countdownText.text = _startingNumber.ToString();
            _startingFontSize = _countdownText.fontSize;
            StartCoroutine(CountdownCor());
        }

    }
    IEnumerator CountdownCor()
    {
        float pct=0;
        for (int i=0;i<_startingNumber;i++)
        {
            while(pct<1)
            {
                _time += Time.deltaTime;
                pct = math.remap(0, _timeForNumberToDisappear, 0, 1, _time);
                pct = math.clamp(pct,0, 1);
                _countdownText.fontSize = math.lerp(_startingFontSize, 0, pct);
                yield return null;
            }
            _currentNumber--;
            _time = 0;
            pct = 0;
            _countdownText.text=_currentNumber.ToString();
            _countdownText.fontSize = _startingFontSize;
        }
        OnCountdownEnd?.Invoke();
        gameObject.SetActive(false);
    }
}
