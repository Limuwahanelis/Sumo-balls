using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(menuName = "Cosmetic")]
[Serializable]
public class CosmeticSO : UnlockableItem
{
    public GameObject Prefab=>_prefab;
    [SerializeField,SearchContext("p: t:Cosmetic prefab:any")] GameObject _prefab;
    public List<Color> Colors => _colors;

    [SerializeField] List<Color> _colors = new List<Color>();

}
