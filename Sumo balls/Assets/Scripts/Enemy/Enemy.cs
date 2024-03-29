using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    public UnityEvent<Enemy> OnDeath;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected float _force;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected GameObject _belt;
}
