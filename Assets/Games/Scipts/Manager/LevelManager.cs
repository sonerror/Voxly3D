using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class LevelManager : Singleton<LevelManager>
{
    public LevelDataTFAssetData levelDataTFAssetData;
    public Level _levelIndex;
    public Level levelCurrent;
    public GameObject voxelPiece;
    public int _IDSelected;
    public bool areDrawing = false;
    public int indexIDLV = 0;
    public List<int> idButton = new List<int>();
    private void OnEnable()
    {
        EventManager.OnLoadNewScene += OnLoadNewScene;
    }

    private void OnDestroy()
    {
        EventManager.OnLoadNewScene -= OnLoadNewScene;
    }

    public void OnLoadNewScene()
    {
        if (SceneController.Ins.currentSceneName.Equals("GamePlay") && !UIManager.Ins.IsOpened<Home>())
        {
            LoadLevel();
        }
        idButton.Sort();
    }

  
    public void LoadLevel()
    {
        DestroyCurrentLevel();
        InstantiateLevel(indexIDLV);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
    }
    public void InstantiateLevel(int indexLevel)
    {
        levelCurrent = Instantiate(_levelIndex);
        LevelData levelDataTF = levelDataTFAssetData.GetLevelDataWithID(indexLevel).levelDatas;
        foreach (TransformData tf in levelDataTF.transformDataList)
        {
            VoxelPiece v = SimplePool.Spawn<VoxelPiece>(PoolType.VoxelPiece);
            v.ID = tf.id;
            v.Oninit();
            v.TF.SetParent(null);
            v.TF.position = tf.position;
            v.TF.SetParent(levelCurrent.container);
        }
    }
    public void DestroyCurrentLevel()
    {
        if (levelCurrent) Destroy(levelCurrent.gameObject);
    }
    public void ChangematFormID()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel != true)
            {
                if (levelCurrent.voxelPieces[i].ID == _IDSelected)
                {
                    MatManager.Ins.ChangeMatNumberSelectCurent(levelCurrent.voxelPieces[i], levelCurrent.voxelPieces[i].ID);
                }
                else
                {
                    MatManager.Ins.ChangeMatNumber(levelCurrent.voxelPieces[i], levelCurrent.voxelPieces[i].ID);
                }
            }
        }
    }
    public void CheckID(int idInput)
    {
        _IDSelected = idInput;
    }
    public int CountVoxelPiecesWithID(int targetID)
    {
        int count = 0;
        foreach (var voxelPiece in levelCurrent.voxelPieces)
        {
            if (voxelPiece.ID == targetID)
            {
                count++;
            }
        }
        return count;
    }
    public void CheckWinLose(int id)
    {
        if (levelCurrent.CountSumVoxel() > 0)
        {
            if (levelCurrent.CountVoxel(id) <= 0)
            {
                Debug.LogError(levelCurrent.CountVoxel(id) + " het id " + id);
            }
        }
        else
        {
            Debug.LogError("Win");
        }
    }
    public void SetMatZoomIn()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel != true)
            {
                MatManager.Ins.ChangeMatNumber(levelCurrent.voxelPieces[i], levelCurrent.voxelPieces[i].ID);
            }
        }
    }
    public void SetMatZoomOut()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel != true)
            {
                ChangeMatCurrent(levelCurrent.voxelPieces[i]);
            }
        }
    }
    public void ChangeMatCurrent(VoxelPiece voxelPiece)
    {
        if (voxelPiece.ID == 1)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 0);
        }
        if (voxelPiece.ID == 2)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 1);
        }
        if (voxelPiece.ID == 3)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 2);
        }
        if (voxelPiece.ID == 4)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 2);
        }
        if (voxelPiece.ID == 5)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 1);
        }
        if (voxelPiece.ID == 6)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 0);
        }
    }
}
public static class TransformDataHelper
{
    public static List<int> GetUniqueIDs(List<TransformData> dataList)
    {
        HashSet<int> uniqueIDs = new HashSet<int>();

        foreach (TransformData data in dataList)
        {
            uniqueIDs.Add(data.id);
        }

        return new List<int>(uniqueIDs);
    }
}