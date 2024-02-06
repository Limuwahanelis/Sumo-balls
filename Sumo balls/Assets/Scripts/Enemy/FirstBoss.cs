using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

public class FirstBoss : Enemy
{
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] GameObject _stunStars;
    [SerializeField] float _stunTime;
    [SerializeField] GameObject _playerBody;
    [SerializeField] AudioPool _audioPool;
    [SerializeField] SingleClipAudioEvent _wallClash;
    [SerializeField] SingleClipAudioEvent _ballClash;
    private bool _isStunned;
    private Material _material;
    public UnityEvent OnStunned;
    public UnityEvent OnKilled;
    bool _push;
    private void Awake()
    {
#if UNITY_EDITOR
        if (_player == null) _player = GameObject.Find("Player body");
#endif
        _player = _playerBody;
        _material = GetComponent<MeshRenderer>().material;
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (_isStunned) return;
        if (GlobalSettings.IsGamePaused) return;
        _rb.AddForce((_player.transform.position - transform.position).normalized * _force * Time.deltaTime);
        if (_rb.position.y < -0.5f || Vector3.Distance(transform.position, Vector3.zero) > 11f)
        {
            OnDeath?.Invoke(this);
        }
        if (_rb.velocity.magnitude > 2.4)
        {
            _push = true;
            _material.EnableKeyword("_EMISSION");
        }
        else
        {
            _material.DisableKeyword("_EMISSION");
            _push = false;
        }
        if (transform.position.y < -0.5f)
        {
            OnKilled?.Invoke();
            enabled = false;
        }
    }
    public void SetPlayer(GameObject player) => _player = player;
    public void Push(Vector3 force)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.AddForce(force, ForceMode.Impulse);
        _ballClash.Play(_audioPool.GetAudioSourceObject().AudioSource);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Player player = collision.collider.GetComponentInParent<Player>();
        if (_wallLayer == (_wallLayer | (1 << collision.collider.gameObject.layer)))
        {
            Debug.Log($"realtive: {collision.relativeVelocity.magnitude}");
            Debug.Log(_rb.velocity.magnitude);
            if (_push)
            {
                _push = false;
                _wallClash.Play(_audioPool.GetAudioSourceObject().AudioSource);
                collision.rigidbody.gameObject.GetComponent<Wall>().EnableGravity();
                collision.rigidbody.AddForceAtPosition(collision.relativeVelocity * -6000f, collision.GetContact(0).point, ForceMode.Force);
                StartCoroutine(StunCor());
            }
        }
        
        Player player = collision.gameObject.GetComponentInParent<Player>();
        if (player!=null)
        {
            Vector3 _playerDirection = (_player.transform.position - transform.position).normalized;
            _playerDirection.y = 0;
            if (Vector3.Dot(_rb.velocity, _playerDirection) >= 0.5)
            {
                player.Squish();
            }
            //else _ballClash.Play(_audioPool.GetAudioSourceObject().AudioSource);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Player player = collision.gameObject.GetComponentInParent<Player>();
        if (player != null)
        {
            Vector3 _playerDirection = (_player.transform.position - transform.position).normalized;
            _playerDirection.y = 0;
            if (Vector3.Dot(_rb.velocity, _playerDirection) >= 0.5)
            {
                player.Squish();
            }
        }
    }
    public void ResetBoss()
    {
        Stop();
        enabled = true;
        _material.DisableKeyword("_EMISSION");
    }
    public void Stop()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
    IEnumerator StunCor()
    {
        OnStunned?.Invoke();
        _material.DisableKeyword("_EMISSION");
        Stop();
        _stunStars.SetActive(true);
        _isStunned = true;
        yield return new WaitForSeconds(_stunTime);
        _isStunned = false;
        _stunStars.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        pos.y = 0.063052f+4.5f;
        Vector3 dd=pos;
        dd.x = 0;
        dd.z = 0;
        Gizmos.DrawLine(pos, (_rb.velocity * 1.5f)+dd);

    }
}
