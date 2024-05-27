using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cosmetics/Cosmetic")]
public class CosmeticSO : UnlockableItem
{
    public GameObject Prefab=>_prefab;
    [SerializeField,SearchContext("p: t:Cosmetic prefab:any")] GameObject _prefab;
    public List<string> PartsNames => _partsNames;
    [SerializeField] List<string> _partsNames = new List<string>();
    public List<Color> Colors => _colors;
    [SerializeField] List<Color> _colors = new List<Color>();
    public Texture CosmeticIcon=> _sprite;
    [SerializeField, SearchContext("p: dir=\"Screenshots from cosmetics\"")] Texture _sprite;

}
