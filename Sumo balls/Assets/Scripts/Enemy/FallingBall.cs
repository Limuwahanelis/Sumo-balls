using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class FallingBall : MonoBehaviour
{
    public UnityEvent<FallingBall> OnHitFloorAndDisappeared;
    public GameObject MainBody=>_mainBody;
    [SerializeField] GameObject _mainBody;
    [SerializeField] ShadowQuad _shadowQuad;
    [SerializeField] TransparentOverTime _transparent;
    private float _arenaHeight;
    private float _fallSpeed;
    private float _startingY;
    private float _maxRadiusForKill;
    bool hit = false;
    private IObjectPool<FallingBall> _pool;
    private void Start()
    {
    }
    public void SetUp(float fallSpeed,float scale,Vector3 position,float arenaHeight,float playerRadius)
    {
        hit = false;
        _arenaHeight=arenaHeight;
        _mainBody.transform.position = position;
        _fallSpeed = fallSpeed;
        _startingY = position.y;
        _mainBody.transform.localScale = new Vector3(scale,scale,scale);
        float b = scale / 2 - playerRadius;
        float c = scale / 2 + playerRadius;
        float a = math.sqrt(c * c - b * b);
        _maxRadiusForKill = a;
        _shadowQuad.SetUp(_maxRadiusForKill*2, a);
        _transparent.ResetTransparency();
        Vector3 shadowQuadPos = new Vector3(position.x, arenaHeight, position.z);
        _shadowQuad.SetPos(shadowQuadPos);

    }
    private void Update()
    {
        float yPos = _mainBody.transform.position.y - _fallSpeed * Time.deltaTime;
        if (hit) return;
        if (yPos < _arenaHeight + _mainBody.transform.localScale.y / 2)
        {
            hit = true;
            _transparent.StartFadeOutCor();
            _transparent.OnFadeOutCorutineEnded.AddListener(CallEvent);
        }
        _mainBody.transform.position = new Vector3(_mainBody.transform.position.x, yPos, _mainBody.transform.position.z);
        float pct = math.unlerp(_arenaHeight, _startingY, _mainBody.transform.position.y-_mainBody.transform.localScale.y/2);
        _shadowQuad.SetRadius(pct);
    }
    private void CallEvent()
    {
        OnHitFloorAndDisappeared?.Invoke(this);
        _transparent.OnFadeOutCorutineEnded.RemoveListener(CallEvent);
    }

    public void ReturnToPool() => _pool.Release(this);

    public void SetPool(IObjectPool<FallingBall> pool) => _pool = pool;
    public float GetScale()
    {
        return _mainBody.transform.localScale.x;
    }
    public Vector3 GetXZPos()
    {
        Vector3 toreturn = new Vector3(_mainBody.transform.position.x, 0, _mainBody.transform.position.z);
        return toreturn;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponentInParent<Player>();
        NormalEnemy enemy = collision.gameObject.GetComponentInParent<NormalEnemy>();
        if (player) player.Squish(true);
        else if (enemy) enemy.Squish();
        
    }
    private void OnDestroy()
    {
        _transparent.OnFadeOutCorutineEnded?.RemoveListener(CallEvent);
    }
}
