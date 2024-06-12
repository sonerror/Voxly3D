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
            SetAcTextAll(false);
        }
    }
    public void LoadLevel()
    {
        DestroyCurrentLevel();
        InstantiateLevel(8);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
    }
    public void InstantiateLevel(int indexLevel)
    {
        levelCurrent = Instantiate(_levelIndex);
        LevelData levelDataTF = levelDataTFAssetData.GetLevelDataWithID(indexLevel).levelDatas;
        foreach(TransformData tf in levelDataTF.transformDataList)
        {
            VoxelPiece v = SimplePool.Spawn<VoxelPiece>(PoolType.VoxelPiece);
            v.TF.SetParent(null);
            v.Oninit();
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
                    Debug.LogError(levelCurrent.voxelPieces[i].ID);
                    MatManager.Ins.ChangeMat(levelCurrent.voxelPieces[i], 1);
                }
                else
                {
                    MatManager.Ins.ChangeMat(levelCurrent.voxelPieces[i], 0);
                }
            }
        }
    }
    public void CheckID(int idInput)
    {
        _IDSelected = idInput;
    }
    public void CheckWinLose()
    {

    }
    public void SetActiveText(int _idInput)
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].ID == _idInput)
            {
                for (int j = 0; j < levelCurrent.voxelPieces[i].textIDUI.Count; j++)
                {
                    levelCurrent.voxelPieces[i].textIDUI[j].gameObject.SetActive(false);
                }
            }
        }
    }
    public void SetAcTextF(VoxelPiece voxelPiece)
    {
        for (int j = 0; j < voxelPiece.textIDUI.Count; j++)
        {
            voxelPiece.textIDUI[j].gameObject.SetActive(false);
        }
    }
    public void SetAcTextAll(bool _isVoxce)
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            for (int j = 0; j < levelCurrent.voxelPieces[i].textIDUI.Count; j++)
            {
                levelCurrent.voxelPieces[i].textIDUI[j].gameObject.SetActive(_isVoxce);
            }
        }
    }
    public void SetAcText()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel == true)
            {
                for (int j = 0; j < levelCurrent.voxelPieces[i].textIDUI.Count; j++)
                {
                    levelCurrent.voxelPieces[i].textIDUI[j].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int j = 0; j < levelCurrent.voxelPieces[i].textIDUI.Count; j++)
                {
                    levelCurrent.voxelPieces[i].textIDUI[j].gameObject.SetActive(true);
                }
            }
        }
    }

}