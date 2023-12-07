using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Action OnDeath;

    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
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
        _rb.AddForce(force,ForceMode.Impulse);
    }
}
