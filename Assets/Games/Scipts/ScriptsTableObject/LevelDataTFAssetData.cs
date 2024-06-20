using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelTFAssetData", menuName = "ScriptsTableObject/LevelTF Asset Data", order = 2)]

public class LevelDataTFAssetData : ScriptableObject
{
    public List<LevelDataTFDataModel> levelDataTFDataModels = new List<LevelDataTFDataModel>();
    public LevelDataTFDataModel GetLevelDataWithID(int _id)
    {
        return levelDataTFDataModels.Find(e => e.id == _id);
    }

}
[System.Serializable]
public class LevelDataTFDataModel
{
    public ObscuredInt id;
    public LevelData levelDatas;
    public Sprite sprite;
    public int timeCount;
}
