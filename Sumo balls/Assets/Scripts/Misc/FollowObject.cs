using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform _transformToFollow;
    [SerializeField] bool _XAxis;
    [SerializeField] bool _YAxis;
    [SerializeField] bool _ZAxis;
    [SerializeField] Vector3 _offset;
    private Vector3 _originalpos;
    // Start is called before the first frame update
    void Start()
    {
        _originalpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = _transformToFollow.position+_offset;
        if (!_XAxis) target.x=_originalpos.x;
        if (!_YAxis) target.y=_originalpos.y;
        if (!_ZAxis) target.z=_originalpos.z;
        transform.position = target;
    }
}
