using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{

    public Action OnDeath;

    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    private GameObject _player;
    private IObjectPool<Enemy> _pool;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player body");
        Debug.Log("fsafsa");
        // _pool.Release(this);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce((_player.transform.position - transform.position).normalized * _speed);
        if (_rb.position.y < -0.5f) OnDeath?.Invoke();
    }
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public void Push(Vector3 force)
    {
        Debug.Log(force);
        _rb.AddForce(force, ForceMode.Impulse);
    }
    public void SetPool(IObjectPool<Enemy> pool) => _pool = pool;
}
