using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnPlayerDeath;
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] GameObject _pivot;
    [SerializeField] GameObject _powerUpIndicator;
    [SerializeField] float _force;
    [SerializeField] float _powerUpStrength = 15f;
    [SerializeField] float _powerUpDuration;
    [SerializeField] private bool _hasPowerUp;
    private bool _hasBroadcastedDeath = false;
    private Coroutine _powerUpCor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused) return;
        if (_hasPowerUp) _powerUpIndicator.transform.position = _playerRB.position + new Vector3(0, -0.6f, 0);
        if (_playerRB.transform.position.y < 0.5f && !_hasBroadcastedDeath)
        {
            OnPlayerDeath?.Invoke();
            _hasBroadcastedDeath = true;
        }
    }
    public void ResetPlayer()
    {
        ResetRigidbody();
        if(_powerUpCor != null)
        {
            StopCoroutine(_powerUpCor);
            _powerUpCor = null;
        }
        _powerUpIndicator.SetActive(false);
        _hasPowerUp = false;
        _hasBroadcastedDeath = false;
    }
    public void ResetRigidbody()
    {
        _playerRB.velocity = Vector3.zero;
        _playerRB.angularVelocity = Vector3.zero;
        _playerRB.transform.localPosition = Vector3.zero;
        
    }
    public void PushBall(float direction)
    {
        _playerRB.AddForce(_pivot.transform.forward*_force*direction);
    }
    public void Collision(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && _hasPowerUp)
        {
            Vector3 pushVector = (enemy.transform.position - _playerRB.position).normalized * _powerUpStrength;
            enemy.Push(pushVector);
        }
    }
    public void Trigger(Collider other)
    {
        if (other.GetComponent<PowerUp>())
        {
            _hasPowerUp = true;
            _powerUpIndicator.SetActive(true);
            if(_powerUpCor!=null)
            {
                StopCoroutine(_powerUpCor);
                _powerUpCor = StartCoroutine(PowerUpCountdownCor());
            }
            else _powerUpCor=StartCoroutine(PowerUpCountdownCor());
            Debug.Log("picked");
        }
    }

    private IEnumerator PowerUpCountdownCor()
    {
        yield return new WaitForSeconds(_powerUpDuration);
        _hasPowerUp = false;
        _powerUpIndicator.SetActive(false);
        _powerUpCor = null;
    }
}
