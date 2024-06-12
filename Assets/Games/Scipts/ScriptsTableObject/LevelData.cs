using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptsTableObject/LevelData", order = 3)]

public class LevelData : ScriptableObject
{
    [SerializeField]
    public List<TransformData> transformDataList = new List<TransformData>();

}
[Serializable]
public class TransformData
{
    public Vector3 position;

    public TransformData(Vector3 position)
    {
        this.position = position;
    }
}
