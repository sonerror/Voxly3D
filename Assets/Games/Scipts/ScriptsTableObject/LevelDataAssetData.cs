using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelsAssetData", menuName = "ScriptsTableObject/Levels Asset Data", order = 4)]

public class LevelDataAssetData : ScriptableObject
{
    public List<LevelDataModel> dataLevels = new List<LevelDataModel>();
    public LevelDataModel GetLevelWithID(int _id)
    {
        return dataLevels.Find(e => e.id == _id);
    }
}
[System.Serializable]
public class LevelDataModel
{
    public ObscuredInt id;
    public Level levels;
}