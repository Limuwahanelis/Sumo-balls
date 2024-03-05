using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class NormalEnemy : Enemy
{
    public Rigidbody Rigidbody => _rb;

    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] SingleClipAudioEvent _squishAudioEvent;
    [SerializeField] Transform _rbParent;
    [SerializeField] ColorList _colors;
    [SerializeField] LayerMask _arenaLayer;
    [SerializeField] AudioPool _audioPool;
    [SerializeField] SingleClipAudioEvent _clashAudioEvent;
    [SerializeField] SingleClipAudioEvent _wallClashAudioEvent;
    [SerializeField] float _changeAngluarDragDistance;
    [SerializeField] float _safetyAngularDrag = 6f;
    private bool _hasChangedAngularDrag;
    private bool _isBeingSquished = false;
    private IObjectPool<NormalEnemy> _pool;
    private MaterialPropertyBlock _materialPropertyBlock;
    private MaterialPropertyBlock _beltMaterialPropertyBlock;
    private Renderer _beltRenderer;
    private float _originalAngularDrag;
    private Coroutine _squishCor; 
    int _hits = 0;
    private void Awake()
    {
        if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
        _beltRenderer = _belt.GetComponent<MeshRenderer>();
        _beltMaterialPropertyBlock = new MaterialPropertyBlock();
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
        _rb.AddForce((_player.transform.position - _rb.position).normalized * _force * Time.deltaTime);
        if ((_rb.position.y < 0.3f || Vector3.Distance(_rb.position,Vector3.zero)>9.5f) && !_isBeingSquished)
        {
            OnDeath?.Invoke(this);
            if (_pool != null) _pool.Release(this);
            else Destroy(gameObject);
        }
        if(!_hasChangedAngularDrag && Vector3.Distance(_rb.position,Vector3.zero)>_changeAngluarDragDistance)
        {
            _hasChangedAngularDrag = true;
            if (_rb.angularDrag < 2.5f) _rb.angularDrag = _safetyAngularDrag + 3f;
            else _rb.angularDrag = _safetyAngularDrag;
            _rb.drag = 2f;
        }
        if(_hasChangedAngularDrag && Vector3.Distance(_rb.position, Vector3.zero) < _changeAngluarDragDistance)
        {
            _hasChangedAngularDrag = false;
            _rb.angularDrag = _originalAngularDrag;
            _rb.drag = 0f;
        }
    }
    public void ResetEnemy()
    {
        _hits = 0;
        _materialPropertyBlock.SetColor("_BaseColor", _colors.colorList[_hits]);
        _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        //_rb.transform.localPosition = Vector3.zero;
        _rbParent.localScale = Vector3.one;
        _rbParent.localPosition = Vector3.zero;
        _rb.isKinematic = false;
        _rb.GetComponent<Collider>().enabled = true;
        _rb.useGravity = true;
        if (_squishCor != null)
        {
            StopCoroutine(_squishCor);
            _squishCor = null;

        }
        _isBeingSquished = false;

    }
    public void SetPlayer(GameObject player)=>_player = player;
    public void Push(Vector3 force)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.AddForce(force, ForceMode.Impulse);
    }
    public void SetRBPos(Vector3 pos)=>_rb.transform.position = pos;
    public void SetAudioPool(AudioPool pool) => _audioPool = pool;
    public void SetPool(IObjectPool<NormalEnemy> pool) => _pool = pool;
    public void SetBelt(EnemyBelts.Belt belt)
    {
        switch (belt)
        {
            case EnemyBelts.Belt.WHITE:_beltMaterialPropertyBlock.SetColor("_BaseColor", Color.white); break;
            case EnemyBelts.Belt.YELLOW: _beltMaterialPropertyBlock.SetColor("_BaseColor", Color.yellow); break;
            case EnemyBelts.Belt.BLACK: _beltMaterialPropertyBlock.SetColor("_BaseColor", Color.black); break;
        }
        _beltRenderer.SetPropertyBlock(_beltMaterialPropertyBlock);
    }
    public void RandomizeAngularDrag(float min,float max)
    {
        _rb.angularDrag = Random.Range(min,max);
        _originalAngularDrag=_rb.angularDrag;
    }
    public void RandomizePushForce(float min, float max)
    {
        _force = Random.Range(min,max);
    }
    private void PlayClashSound()
    {
        AudioSourceObject audioObject = _audioPool.GetAudioSourceObject();
        audioObject.AudioSource.pitch = Random.Range(0.85f, 1.1f);
        _clashAudioEvent.Play(audioObject.AudioSource);
        audioObject.ReturnToPool(0.5f);
    }
    public void ReturnToPool() =>_pool.Release(this);

    #region Squish
    public void Squish()
    {
        _squishCor=StartCoroutine(SquishCor());

    }
    IEnumerator SquishCor()
    {
        if (_isBeingSquished) yield break;
        _squishAudioEvent.Play(_audioPool.GetAudioSourceObject().AudioSource);
        _isBeingSquished = true;
        float squishEndYPos = -0.48f;
        float rigidbodyOffset = _rb.transform.localPosition.y;
        Vector3 squishPos = _rbParent.transform.localPosition;
        Vector3 scale = new Vector3(1, 1, 1);
        float yPos = squishPos.y;
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _rb.GetComponent<Collider>().enabled = false;
        StopEnemy();
        for (float time = 0; time < 0.40f; time += Time.deltaTime)
        {
            squishPos.y = math.lerp(yPos, squishEndYPos+ rigidbodyOffset, time / 0.40f);
            scale.y = math.lerp(1, 0, time / 0.40f);
            _rbParent.localScale = scale;
            _rbParent.transform.localPosition = squishPos;
            yield return null;
        }
        _isBeingSquished = false;
        OnDeath?.Invoke(this);
        if (_pool != null) _pool.Release(this);
        else Destroy(gameObject);
    }
    private void StopEnemy()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
    #endregion
    public void OnColEnter(Collision collision)
    {
        OnCollisionEnter(collision);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!(_arenaLayer == (_arenaLayer | (1 << collision.collider.gameObject.layer))))
        {
            Vector3 _direction = (collision.gameObject.transform.position - _rb.position).normalized;
            _rb.AddForce( _hits * 0.2f * (Vector3.Dot(_direction, collision.impulse.normalized)>0?-collision.impulse:collision.impulse), ForceMode.Impulse);

        }
        else
        {
            AudioSourceObject audioObject = _audioPool.GetAudioSourceObject();
            _wallClashAudioEvent.Play(audioObject.AudioSource);
            audioObject.ReturnToPool(0.5f);
        }
        if (collision.gameObject.GetComponentInParent<Player>()) 
        {
            PlayClashSound();
            _hits++;
            if (_hits > 3) _hits = 3;
            Vector3 _direction = (collision.gameObject.transform.position - _rb.position).normalized;
            _rb.AddForce(0.4f * (Vector3.Dot(_direction, collision.impulse.normalized) > 0 ? -collision.impulse : collision.impulse), ForceMode.Impulse);
            _materialPropertyBlock.SetColor("_BaseColor", _colors.colorList[_hits]);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        } 
        if(collision.gameObject.GetComponent<NormalEnemy>())
        {
            PlayClashSound();
        }
    }
    private void OnValidate()
    {
        if (_meshRenderer == null) _meshRenderer = GetComponent<MeshRenderer>();
    }
}
