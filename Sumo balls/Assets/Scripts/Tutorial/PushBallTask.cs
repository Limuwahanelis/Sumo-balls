using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushBallTask : MonoBehaviour
{
    [SerializeField] Transform _playerBody;
    [SerializeField] float _radiusToleave = 4f;
    private Vector3 _playerStartingPos;

    public UnityEvent OnTaskCompleted;
    private void Awake()
    {
        _playerStartingPos = _playerBody.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_playerBody.position, _playerStartingPos) >= _radiusToleave)
        {
            OnTaskCompleted?.Invoke();
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_playerStartingPos, _radiusToleave);
    }
}
