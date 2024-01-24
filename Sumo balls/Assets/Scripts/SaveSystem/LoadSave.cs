using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    [SerializeField] bool _overrideSave;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveGameData.LoadGameData() == false || _overrideSave)
        {
            SaveGameData.CreateGameData(_stageList.stages);
            int i = 0;
            foreach (StageData data in SaveGameData.GameData.stagesData)
            {
                if (data.completed) _stageList.stages[i].SetScore(0);
                i++;
            }
        }
        else
        {
            int i = 0;
            foreach (StageData data in SaveGameData.GameData.stagesData)
            {
                if (data.completed) _stageList.stages[i].SetScore(data.score);
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
