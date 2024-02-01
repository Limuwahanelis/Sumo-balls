using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class NormalEnemy : Enemy
{

    [SerializeField] ColorList _colors;
    [SerializeField] LayerMask _arenaLayer;
    [SerializeField] AudioPool _audioPool;
    [SerializeField] SingleClipAudioEvent _clashAudioEvent;
    private IObjectPool<NormalEnemy> _pool;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    int _hits = 0;
    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _materialPropertyBlock=new MaterialPropertyBlock();
#if UNITY_EDITOR
        if (_player==null) _player = GameObject.Find("Player body");

#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalSettings.IsGamePaused) return;
        _rb.AddForce((_player.transform.position - transform.position).normalized * _force * Time.deltaTime);
        if (_rb.position.y < -0.5f || Vector3.Distance(transform.position,Vector3.zero)>11f)
        {
            OnDeath?.Invoke(this);
            _pool.Release(this);
        }
    }
    public void ResetEnemy()
    {
        _hits = 0;
        _materialPropertyBlock.SetColor("_BaseColor", _colors.colorList[_hits]);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    public void SetPlayer(GameObject player)=>_player = player;
    public void Push(Vector3 force)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.AddForce(force, ForceMode.Impulse);
    }
    public void SetAudioPool(AudioPool pool) => _audioPool = pool;
    public void SetPool(IObjectPool<NormalEnemy> pool) => _pool = pool;

    public void RandomizeAngularDrag(float min,float max)
    {
        _rb.angularDrag = Random.Range(min,max);
    }
    public void RandomizePushForce(float min, float max)
    {
        _force = Random.Range(min,max);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!(_arenaLayer == (_arenaLayer | (1 << collision.collider.gameObject.layer))))
        {
            Vector3 _direction = (collision.gameObject.transform.position - transform.position).normalized;
            _rb.AddForce( _hits * 0.2f * (Vector3.Dot(_direction, collision.impulse.normalized)>0?-collision.impulse:collision.impulse), ForceMode.Impulse);
        }
        if (collision.gameObject.GetComponentInParent<Player>()) 
        {
            AudioSourceObject audioObject= _audioPool.GetAudioSourceObject();
            audioObject.AudioSource.pitch= Random.Range(0.85f, 1.1f);
            _clashAudioEvent.Play(audioObject.AudioSource);
            audioObject.ReturnToPool(0.5f);
            _hits++;
            if (_hits > 3) _hits = 3;
            
            _materialPropertyBlock.SetColor("_BaseColor", _colors.colorList[_hits]);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        } 
    }
    private void OnValidate()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
    }
}
