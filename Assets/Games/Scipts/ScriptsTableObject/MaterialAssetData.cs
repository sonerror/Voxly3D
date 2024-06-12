using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialAssetData", menuName = "ScriptsTableObject/Material Asset Data", order = 1)]
public class MaterialAssetData : ScriptableObject
{
    public List<MaterialDataModel> matDataModels = new List<MaterialDataModel>();
    public MaterialDataModel GetMatWithID(int _id)
    {
        return matDataModels.Find(e => e.id == _id);    
    }
}
[System.Serializable]
public class MaterialDataModel
{
    public ObscuredInt id;
    public Material mats;
    public void CoppyFormOther(MaterialDataModel other)
    {
        id = other.id;
        mats = other.mats;
    }
}
