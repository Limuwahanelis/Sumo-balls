using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewCamera : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _cameraPivot;
    private float _pitch;
    private float _yaw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dd(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        float xRotation = pointerEventData.delta.x * _rotationSpeed;
        float yRotation = -pointerEventData.delta.y * _rotationSpeed;
        _yaw += xRotation;
        _pitch += yRotation;
        if (_pitch < -20) _pitch = -20;
        if (_pitch > 60) _pitch = 60;
        _cameraPivot.transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}
