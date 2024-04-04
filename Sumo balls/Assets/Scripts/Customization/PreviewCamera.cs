using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewCamera : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] Transform _cameraPivot;

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
        _cameraPivot.transform.Rotate(Vector3.up, xRotation);

    }
}
