using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    public UnityEvent OnWallCollapsed;
    public UnityEvent OnWallFell;
    [SerializeField] Rigidbody _rb;
    private Vector3 _originalPos;
    private Quaternion _originalRot;
    private bool _isCollapsed = false;
    private RigidbodySettings _rbOriginalSettings;
   struct RigidbodySettings
    {
        public bool useGravity;
        public bool isKinematic;
        public void Apply(Rigidbody rb)
        {
            rb.useGravity = useGravity;
            rb.isKinematic = isKinematic;
        }
    }
    public void SetUp()
    {
        _originalPos = transform.position;
        _originalRot = transform.rotation;

    }
    private void Awake()
    {
        if(_rb==null)_rb = GetComponent<Rigidbody>();
        _rbOriginalSettings = new RigidbodySettings()
        {
            useGravity = _rb.useGravity,
            isKinematic = _rb.isKinematic
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.forward, Vector3.up) >= 0.8 && !_isCollapsed)
        {
            OnWallCollapsed?.Invoke();
            _isCollapsed = true;
        }
        if (transform.position.y <= -4f)
        {
            gameObject.SetActive(false);
            OnWallFell?.Invoke();
            if (!_isCollapsed)
            {
                _isCollapsed = true;
                OnWallCollapsed?.Invoke();
            }
        }
    }

    public void Restore()
    {
        _rb.isKinematic = _rbOriginalSettings.isKinematic;
        _rb.useGravity = _rbOriginalSettings.useGravity;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _isCollapsed = false;
        transform.position = _originalPos;
        transform.rotation = _originalRot;
        gameObject.SetActive(true);
    }

    public void EnableGravity()
    {
        _rb.isKinematic = false;
        _rb.useGravity = true;
    }
}
