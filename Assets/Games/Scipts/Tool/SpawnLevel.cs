using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;

#if UNITY_EDITOR
public class SpawnLevel : EditorWindow
{
    private int sizeX = 3;
    private int sizeY = 3;
    private int sizeZ = 3;
    private Transform tf;
    private List<Vector3> cubePositions = new List<Vector3>();
    private LevelDataTFAssetData levelDataAsset;
    private int levelID;
    private LevelData levelData;

    [MenuItem("Tools/Level Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SpawnLevel));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save Cube Positions"))
        {
            SaveCubePositions();
        }
        GUILayout.Label("Cube Grid Settings", EditorStyles.boldLabel);

        sizeX = EditorGUILayout.IntField("Size X", sizeX);
        sizeY = EditorGUILayout.IntField("Size Y", sizeY);
        sizeZ = EditorGUILayout.IntField("Size Z", sizeZ);
        tf = EditorGUILayout.ObjectField("Container:", tf, typeof(Transform), true) as Transform;

        if (GUILayout.Button("Generate Cube Positions"))
        {
            GenerateCubePositions();
        }

        GUILayout.Space(20);

        GUILayout.Label("Cube Positions:", EditorStyles.boldLabel);

        foreach (Vector3 pos in cubePositions)
        {
            GUILayout.Label(pos.ToString());
        }

        GUILayout.Space(20);

        GUILayout.Label("Save Settings", EditorStyles.boldLabel);
        levelDataAsset = (LevelDataTFAssetData)EditorGUILayout.ObjectField("Level Data Asset", levelDataAsset, typeof(LevelDataTFAssetData), false);
        levelID = EditorGUILayout.IntField("Level ID", levelID);
    }

    private void GenerateCubePositions()
    {
        cubePositions.Clear();

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1 || z == 0 || z == sizeZ - 1)
                    {
                        if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1 || z == 0 || z == sizeZ - 1)
                        {
                            Vector3 position = new Vector3(x, y, z);
                            cubePositions.Add(position);

                            if (tf != null)
                            {
                                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                cube.transform.position = position;
                                cube.transform.parent = tf;
                            }
                        }
                    }
                }
            }
        }
    }


    private void SaveCubePositions()
    {
        string folderPath = "Assets/Games/OS/Level";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError("Folder path is invalid.");
            return;
        }

        string levelDataPath = folderPath + "/LevelData.asset";
        LevelData levelData = AssetDatabase.LoadAssetAtPath<LevelData>(levelDataPath);
        if (levelData == null)
        {
            levelData = ScriptableObject.CreateInstance<LevelData>();
            AssetDatabase.CreateAsset(levelData, levelDataPath);
        }

        levelData.transformDataList.Clear();

        foreach (Vector3 pos in cubePositions)
        {
            levelData.transformDataList.Add(new TransformData(pos));
        }

        EditorUtility.SetDirty(levelData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Cube positions saved.");
    }
}
#endif
