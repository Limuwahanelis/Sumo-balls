using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Stages list")]
public class StageList : ScriptableObject
{
    public List<Stage> stages=>_stages;
    [SerializeField]  List<Stage> _stages=new List<Stage>();
}
