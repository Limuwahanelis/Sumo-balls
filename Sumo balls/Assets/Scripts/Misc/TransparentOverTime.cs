using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TransparentOverTime : MonoBehaviour
{
    [SerializeField] Color _originalColor;
    [SerializeField] float _fadeOutTime;
    private Renderer _renderer;
    private Color _newColor;
    private MaterialPropertyBlock _block;
    private Coroutine _cor;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if(_block == null) _block = new MaterialPropertyBlock();
        _block.SetColor("_BaseColor", _originalColor);
        _renderer.SetPropertyBlock(_block);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetTransparency()
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
            _cor = null;
        }
        _block.SetColor("_BaseColor", _originalColor);
        _renderer.SetPropertyBlock(_block);
    }
    public void StartFadeOutCor()
    {

        _cor=StartCoroutine(FadeCor());
    }
    private IEnumerator FadeCor()
    {
        float pct = 0;
        _newColor = _originalColor;
        for (float i= _fadeOutTime; i>0;i-=Time.deltaTime)
        {
            pct = math.remap(0, _fadeOutTime, 0, 1, i);
            _newColor.a = pct;
            _block.SetColor("_BaseColor", _newColor);
            _renderer.SetPropertyBlock(_block);
            yield return null;
        }
        _newColor.a = 0;
        _block.SetColor("_BaseColor", _newColor);
        _renderer.SetPropertyBlock(_block);
    }
    private void OnValidate()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        if(_block==null) _block = new MaterialPropertyBlock();
        //MaterialPropertyBlock _block = new MaterialPropertyBlock();
        _block.SetColor("_BaseColor", _originalColor);
        _renderer.SetPropertyBlock(_block);
    }
}
