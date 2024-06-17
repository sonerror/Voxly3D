using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class LevelManager : Singleton<LevelManager>
{
    public LevelDataTFAssetData levelDataTFAssetData;
    public LevelDataAssetData levelDataAssetData;

    public Level _levelIndex;
    public Level levelCurrent;
    public GameObject voxelPiece;
    public int iDSelected;
    public bool areDrawing = false;
    public int indexLevel = 0;
    public List<int> idButton = new List<int>();
    public List<Level> levels25D = new List<Level>();
    public bool isSpawnPrefas = false;
    public List<int> listID = new List<int>();

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
        if (isSpawnPrefas == true)
        {
            LoadLevelPrefabs();
        }
        else
        {
            if (SceneController.Ins.currentSceneName.Equals("GamePlay") && !UIManager.Ins.IsOpened<Home>())
            {
                LoadLevel();
            }
        }
    }
    public void LoadLevel()
    {
        DestroyCurrentLevel();
        InstantiateLevel(indexLevel);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
    }
   
    public void LoadLevelPrefabs()
    {
        DestroyCurrentLevel();
        InstantiateLevelPrefabs(indexLevel);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
        StartCoroutine(SetIDToVoxel());
        StartCoroutine(IE_LoadCellButtonSwatch());
    }


    IEnumerator IE_LoadCellButtonSwatch()
    {
        if(UIManager.Ins.IsOpened<Home>() == false)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
        }
    }
    IEnumerator SetIDToVoxel()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        Dictionary<string, int> materialToID = new Dictionary<string, int>();
        int currentID = 1;
        listID.Clear();
        foreach (var voxelPiece in levelCurrent.voxelPieces)
        {
            string materialName = voxelPiece.material.name;

            if (!materialToID.ContainsKey(materialName))
            {
                materialToID[materialName] = currentID;
                listID.Add(currentID);
                currentID++;
            }

            voxelPiece.ID = materialToID[materialName];
        }
    }

    public void InstantiateLevelPrefabs(int indexLevel)
    {
        levelCurrent = Instantiate(levelDataAssetData.GetLevelWithID(indexLevel).level);
    }


    public void NextLevel()
    {
        indexLevel++;
        SceneController.Ins.LoadCurrentScene(() =>
        {
        }, false, false);
    }
    public void InstantiateLevel25D(int indexLevel)
    {
        levelCurrent = Instantiate(levels25D[indexLevel]);
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
        if (levelCurrent != null)
        {
            iDSelected = 0;
            Destroy(levelCurrent.gameObject);
            Debug.LogError("Destroy = = = = = = = ");
        }

    }
    public void ChangematFormID()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel != true)
            {
                if (levelCurrent.voxelPieces[i].ID == iDSelected)
                {
                    levelCurrent.voxelPieces[i].mesh.material.color = new Color(0.25f, 0.25f, 0.25f);
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
        iDSelected = idInput;
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
                listID.Remove(id);
                iDSelected = listID[0];
                UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
                Debug.LogError(levelCurrent.CountVoxel(id) + " het id " + id);

            }
        }
        else
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Win>();
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
        if (voxelPiece.ID == 7)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 1);

        }
        if (voxelPiece.ID == 8)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 2);
        }
        if (voxelPiece.ID == 9)
        {
            MatManager.Ins.ChangeMatCurent(voxelPiece, 1);
        }
        if (voxelPiece.ID == 10)
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