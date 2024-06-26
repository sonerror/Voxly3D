using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptsTableObject/LevelData", order = 3)]

public class LevelData : ScriptableObject
{
    public int levelID;
    public List<TransformData> tfData;
    public List<MaterialData> materials;
    public float minOrthoSize;
    public float maxOrthoSize;
    public LevelData DeepCopy()
    {
        LevelData copy = CreateInstance<LevelData>();
        copy.levelID = this.levelID;
        copy.tfData = new List<TransformData>(this.tfData.Count);
        foreach (TransformData tf in this.tfData)
        {
            copy.tfData.Add(new TransformData(tf.position, tf.realColorID));
        }
        copy.materials = new List<MaterialData>(this.materials.Count);
        foreach (MaterialData mat in this.materials)
        {
            copy.materials.Add(new MaterialData(mat.material, mat.colorID));
        }
        copy.maxOrthoSize = this.maxOrthoSize;
        copy.minOrthoSize = this.minOrthoSize;
        return copy;
    }
}
[Serializable]
public class TransformData
{
    public Vector3 position;
    public int realColorID;
    public TransformData(Vector3 position, int realColorID)
    {
        this.position = position;
        this.realColorID = realColorID;
    }
}
[System.Serializable]
public class MaterialData
{
    public Material material;
    public int colorID;
    public MaterialData(Material material, int colorId)
    {
        this.material = material;
        this.colorID = colorId;
    }
}