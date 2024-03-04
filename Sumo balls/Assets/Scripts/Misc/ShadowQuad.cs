using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyBelts;

public class ShadowQuad : MonoBehaviour
{
    [SerializeField] GameObject _shadowQuad;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float _radius;
    private float _maxRadius;
    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    public void SetUp(float quadScale,float maxRadius)
    {
        _maxRadius = maxRadius;
        _shadowQuad.transform.localScale = new Vector3(quadScale, quadScale, quadScale);
        _materialPropertyBlock.SetFloat("_MaxRadius", maxRadius);
        _materialPropertyBlock.SetFloat("_Radius", 0);
    }
    public void SetPos(Vector3 position)
    {
        transform.position = position;
    }
    public void SetRadius(float pct)
    {
        _radius=_maxRadius*pct;
        _materialPropertyBlock.SetFloat("_Radius", _radius);
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
