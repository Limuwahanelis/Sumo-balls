using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class TransparentOverTime : MonoBehaviour
{
    public UnityEvent OnFadeOutCorutineEnded;
    public float Currenttransparency => _currentTransparency;
    private float _currentTransparency;
    [SerializeField] Color _originalColor;
    [SerializeField] float _fadeOutTime;
    [SerializeField] bool _fadeSmoothness;
    private Renderer _renderer;
    private Color _newColor;
    private MaterialPropertyBlock _block;
    private Coroutine _cor;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if(_block == null) _block = new MaterialPropertyBlock();
        _block.SetColor("_BaseColor", _originalColor);
        _currentTransparency = _originalColor.a;
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
        if (_fadeSmoothness) _block.SetFloat("_Smoothness", 0.5f);
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
            pct = math.remap(0, _fadeOutTime, 0, 1-(1-_originalColor.a), i);
            if(_fadeSmoothness)
            {
                if (pct <= 0.5f) _block.SetFloat("_Smoothness", pct);

            }
            _newColor.a = pct;
            _currentTransparency=pct;
            _block.SetColor("_BaseColor", _newColor);
            _renderer.SetPropertyBlock(_block);
            yield return null;
        }
        _newColor.a = 0;
        _currentTransparency = 0;
        _block.SetColor("_BaseColor", _newColor);
        _renderer.SetPropertyBlock(_block);
        OnFadeOutCorutineEnded?.Invoke();
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
