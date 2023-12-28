using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
    [SerializeField] InputActionReference _moveMouseAction;
    [SerializeField] PlayerCamera _camera;
    bool _isRotating = false;
    float _rotValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRotating) _camera.RotateKeyboard(_rotValue);
    }
    void OnMouseDelta(InputValue value)
    {
        _camera.RotateMouse(value.Get<Vector2>().x);
    }
    void OnRotateX(InputValue value)
    {
        _rotValue = value.Get<float>();
        if (math.abs( _rotValue)>=1) _isRotating=true;
        else _isRotating=false;
        
    }
}
