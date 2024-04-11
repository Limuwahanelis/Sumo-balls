using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cosmetic : MonoBehaviour
{
    [SerializeField] Vector3 _cosmeticPositionWithRegardToPlayer;
    [SerializeField] MeshRenderer[] _meshRenderers;
    private MaterialPropertyBlock _materialPropertyBlock;
    private void Start()
    {
        _meshRenderers=GetComponentsInChildren<MeshRenderer>();
        
    }
    public void SpawnCosmeticLocally()
    {
        transform.localPosition = _cosmeticPositionWithRegardToPlayer;
    }
    public void SetColors(List<Color> colors)
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
        int i = 0;
        foreach (var color in colors)
        {
            _materialPropertyBlock.SetColor("_BaseColor", color);
            _meshRenderers[i].SetPropertyBlock(_materialPropertyBlock);
            i++;
        }
    }
}
