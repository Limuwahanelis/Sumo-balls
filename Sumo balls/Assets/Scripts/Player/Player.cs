using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] GameObject _pivot;
    [SerializeField] GameObject _powerUpIndicator;
    [SerializeField] Transform _mainBodyParent;
    [SerializeField] AudioPool _audioPool;
    [SerializeField] float _force;
    [SerializeField] float _powerUpStrength = 15f;
    [SerializeField] float _powerUpDuration;
    [SerializeField] private bool _hasPowerUp;
    [SerializeField] LayerMask _arenaLayer;
    [SerializeField] SingleClipAudioEvent _wallClashAudioEvent;
    [SerializeField] SingleClipAudioEvent _bassSquishAudioEvent;
    [SerializeField] SpeedBar _speedBar;
    private bool _hasBroadcastedDeath = false;
    private Coroutine _powerUpCor;
    private Vector3 _startingScale;
    bool sq = false;
    public UnityEvent OnPlayerDeath;

    private void Awake()
    {
        _startingScale = _mainBodyParent.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused) return;
        if (_hasPowerUp) _powerUpIndicator.transform.position = _playerRB.position + new Vector3(0, -0.6f, 0);
        if (_playerRB.transform.localPosition.y < -0.5f && !_hasBroadcastedDeath)
        {
            _hasBroadcastedDeath = true;
            OnPlayerDeath?.Invoke();
        }
        _speedBar.SetSpeed(_playerRB.velocity.magnitude);
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
        _mainBodyParent.localScale = _startingScale;
        _mainBodyParent.localPosition= Vector3.zero;
        _playerRB.GetComponent<Collider>().enabled = true;
        sq = false;
    }
    public void ResetRigidbody()
    {
        StopPlayer();
        _playerRB.transform.localPosition = Vector3.zero;
        _playerRB.useGravity = true;
        _playerRB.isKinematic = false;
    }
    public void PushBall(float direction)
    {
        //Debug.Log(_force * 100 * Time.deltaTime);
        _playerRB.AddForce(_pivot.transform.forward*_force*direction*Time.deltaTime);
    }
    public void StopPlayer()
    {
        _playerRB.velocity = Vector3.zero;
        _playerRB.angularVelocity = Vector3.zero;
    }
    public void Collision(Collision collision)
    {
        NormalEnemy enemy = collision.gameObject.GetComponentInParent<NormalEnemy>();
        FirstBoss boss = collision.gameObject.GetComponent<FirstBoss>();
        if (_arenaLayer == (_arenaLayer | (1 << collision.collider.gameObject.layer)))
        {
            _wallClashAudioEvent.Play(_audioPool.GetAudioSourceObject().AudioSource);
        }
        if (enemy && _hasPowerUp)
        {
            Vector3 pushVector = (enemy.Rigidbody.position - _playerRB.position).normalized * _powerUpStrength;
            enemy.Push(pushVector);
        }
        else if (boss && _hasPowerUp)
        {
            Vector3 pushVector = (boss.transform.position - _playerRB.position).normalized * _powerUpStrength * 50;
            pushVector.y = 0;
            boss.Push(pushVector);
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

    public void Squish(bool isForced = false)
    {
        if (!isForced &&_hasPowerUp) return;
        
        StartCoroutine(SquishCor());
    }
    IEnumerator SquishCor()
    {
        if (sq) yield break;
        //_bassSquishAudioEvent.Play(_audioPool.GetAudioSourceObject().AudioSource);
        sq = true;
        float squishEndYPos = -0.495f;
        Vector3 squishPos = _mainBodyParent.transform.localPosition;
        Vector3 scale = new Vector3(1, 1, 1);
        float yPos = squishPos.y;
        _playerRB.useGravity = false;
        _playerRB.isKinematic = true;
        _playerRB.GetComponent<Collider>().enabled = false;
        StopPlayer();
        //_playerRB.transform.rotation = Quaternion.identity;
        for(float time=0;time<0.40f;time+=Time.deltaTime)
        {
            squishPos.y = math.lerp(yPos, squishEndYPos, time / 0.40f);
            scale.y = math.lerp(1, 0, time / 0.40f);
            _mainBodyParent.localScale = scale;
            _mainBodyParent.transform.localPosition = squishPos;
            yield return null;
        }
        OnPlayerDeath?.Invoke();
    }

    private void OnValidate()
    {
        if (_pivot == null)
        {
            PlayerCamera cam = FindObjectOfType<PlayerCamera>();
            if (cam)_pivot= cam.transform.parent.gameObject;
        }
    }
}
