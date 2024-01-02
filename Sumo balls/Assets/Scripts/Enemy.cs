using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{

    public Action<Enemy> OnDeath;

    [SerializeField] Rigidbody _rb;
    [SerializeField] float _force;
    private GameObject _player;
    private IObjectPool<Enemy> _pool;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        if(_player==null) _player = GameObject.Find("Player body");

#endif
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce((_player.transform.position - transform.position).normalized * _force*Time.deltaTime);
        if (_rb.position.y < -0.5f)
        {
            OnDeath?.Invoke(this);
            _pool.Release(this);
        }
    }
    public void SetPlayer(GameObject player)=>_player = player;
    public void Push(Vector3 force)=> _rb.AddForce(force, ForceMode.Impulse);
    public void SetPool(IObjectPool<Enemy> pool) => _pool = pool;
}
