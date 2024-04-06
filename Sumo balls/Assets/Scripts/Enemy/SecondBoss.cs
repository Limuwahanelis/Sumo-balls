using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SecondBoss : Enemy
{
    [SerializeField] TimeCounter _timeCounter;
    [SerializeField] Transform _bodyParent;
    [SerializeField] float _jumpTime;
    [SerializeField] float _towardsPlayerSpeed;
    [SerializeField] Animator _anim;
    [SerializeField] ShadowQuad _shadow;
    [SerializeField] FloatReference _playerScale;
    [SerializeField] float _arenaHeight;
    // Start is called before the first frame update
    void Start()
    {
        //AimAtPlayer();
        _anim.SetTrigger("Jump");
       // _timeCounter.SetCountTime(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeCounter.CurrentTime>_jumpTime)
        {
            AimAtPlayer();
            
            _timeCounter.ResetTimer();
            _timeCounter.SetCountTime(false);
        }
    }

    public void ResetBoss()
    {
        _timeCounter.ResetTimer();
        _timeCounter.SetCountTime(true);
    }
    public void AimAtPlayer()
    {
        Debug.Log("aim");
        _anim.enabled = false;
        _rb.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _bodyParent.transform.position = new Vector3(_player.transform.position.x, _bodyParent.transform.position.y, _player.transform.position.z);
        float b = 4 / 2 - _playerScale.value/2;
        float c = 4 / 2 + _playerScale.value/2;
        float a = math.sqrt(c * c - b * b);
        float _maxRadiusForKill = a;
        _shadow.SetUp(_maxRadiusForKill * 2, a);
        Vector3 shadowQuadPos = new Vector3(_bodyParent.transform.position.x, _arenaHeight, _bodyParent.transform.position.z);
        _shadow.SetPos(shadowQuadPos);
        _shadow.gameObject.SetActive(true);
    }
    IEnumerator FallingCor()
    {
        yield return null;
    }
}
