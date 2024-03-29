using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnemyBelts;

public class ShadowQuad : MonoBehaviour
{
    [SerializeField] GameObject _shadowQuad;
    [SerializeField] bool _changeColor;
    [SerializeField] Color _color;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float _radius;
    private float _maxRadius;
    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        if (_materialPropertyBlock == null) _materialPropertyBlock = new MaterialPropertyBlock();
        if (_changeColor)
            _materialPropertyBlock.SetColor("_MainColor", _color);
    }
    public void SetUp(float quadScale,float maxRadius)
    {
        _maxRadius = maxRadius;
        _shadowQuad.transform.localScale = new Vector3(quadScale, quadScale, quadScale);
        _materialPropertyBlock.SetFloat("_MaxRadius", maxRadius);
        _materialPropertyBlock.SetFloat("_Radius", 0);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    public void SetPos(Vector3 position)
    {
        transform.position = position;
    }
    public void SetRadius(float pct,bool rawValue=false)
    {
        if(rawValue) _radius = pct;
        else _radius =_maxRadius*pct;
        _materialPropertyBlock.SetFloat("_Radius", _radius);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    private void OnValidate()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        if (_materialPropertyBlock == null) _materialPropertyBlock = new MaterialPropertyBlock();
        if(_changeColor)_materialPropertyBlock.SetColor("_MainColor", _color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
