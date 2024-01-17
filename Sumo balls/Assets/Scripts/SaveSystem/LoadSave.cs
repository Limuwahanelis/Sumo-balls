using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField] StageList _stageList;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveGameData.LoadGameData() == true)
        {
            int i = 0;
            foreach(StageData data in SaveGameData.GameData.stagesData)
            {
                if (data.completed) _stageList.stages[i].SetScore(data.score);
                i++;
            }
        }
        else
        {
            SaveGameData.CreateGameData(_stageList.stages);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
