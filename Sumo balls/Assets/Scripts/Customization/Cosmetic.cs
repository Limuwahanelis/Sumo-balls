using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmetic : MonoBehaviour
{
    [SerializeField] Vector3 _cosmeticPositionWithRegardToPlayer;
    [SerializeField] List<MeshRenderer> _meshRenderers= new List<MeshRenderer>();


    public void SpawnCosmeticLocally()
    {
        transform.localPosition = _cosmeticPositionWithRegardToPlayer;
    }
}
