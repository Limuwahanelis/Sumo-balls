using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraTutorialTask : MonoBehaviour
{
    [SerializeField] float _angleToRotateLeft;
    [SerializeField] float _angleToRotateRight;

    float _plusYaw=0;
    float _minusYaw=0;

    float screenX = Screen.width;
    float screenY = Screen.height;

   [SerializeField] bool _rotatedRight;
   [SerializeField] bool _rotatedLeft;

    public UnityEvent OnTaskCompleted;
    bool _isCompleted;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Mouse.current.position.x.value;
        float mouseY = Mouse.current.position.y.value;
        if (mouseX < 0 || mouseX > screenX || mouseY < 0 || mouseY > screenY)
            return;
    }

    public void RotateKeyboard(float value)
    {
        if (value > 0) _plusYaw++;
        else _minusYaw++;

        if (_plusYaw >= _angleToRotateRight) _rotatedRight = true;
        if(_minusYaw >= _angleToRotateLeft) _rotatedLeft = true;
        if (_rotatedRight && _rotatedLeft && !_isCompleted)
        {
            _isCompleted = true;
            Debug.Log("fasfaf");
            OnTaskCompleted?.Invoke();
        }
    }

    public void RotateMouse(float value)
    {
        if (value > 0) _plusYaw++;
        else _minusYaw++;
        if (_plusYaw >= _angleToRotateRight) _rotatedRight = true;
        if (_minusYaw >= _angleToRotateLeft) _rotatedLeft = true;
        if (_rotatedRight && _rotatedLeft &&!_isCompleted)
        {
            _isCompleted = true;
            Debug.Log("fasfaf");
            OnTaskCompleted?.Invoke();
        }
    }

}
