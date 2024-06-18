using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public bool isSpawnPrefas = false;
    public List<int> listIDs = new List<int>();
    public bool isChangeMatSl = false;

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
        StartCoroutine(IE_LoadCellButtonSwatch());
        isChangeMatSl = false;
    }

    public void LoadLevelPrefabs()
    {
        DestroyCurrentLevel();
        InstantiateLevelPrefabs(indexLevel);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
        //StartCoroutine(SetIDToVoxel());
        StartCoroutine(IE_LoadCellButtonSwatch());
    }


    IEnumerator IE_LoadCellButtonSwatch()
    {
        if (UIManager.Ins.IsOpened<Home>() == false)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
        }
    }
    /*    IEnumerator SetIDToVoxel()
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
        }*/

    public void InstantiateLevelPrefabs(int indexLevel)
    {
        levelCurrent = Instantiate(levelDataAssetData.GetLevelWithID(indexLevel).level);
    }


    public void NextLevel()
    {
        indexLevel++;
        iDSelected = 0;
        SceneController.Ins.LoadCurrentScene(() =>
        {
        }, false, false);
    }

    public void InstantiateLevel(int indexLevel)
    {
        levelCurrent = Instantiate(_levelIndex);
        LevelData levelDataTF = levelDataTFAssetData.GetLevelDataWithID(indexLevel).levelDatas;
        MatManager.Ins.listID = new List<MaterialData>(levelDataTF.materials);
        listIDs = MatManager.Ins.listID.Select(m => m.colorID).ToList();
        foreach (TransformData tf in levelDataTF.tfData)
        {
            VoxelPiece v = SimplePool.Spawn<VoxelPiece>(PoolType.VoxelPiece);
            v.ID = tf.realColorID;
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
        }
    }
    public void ChangematFormID()
    {
        if (isChangeMatSl == true)
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
                listIDs.Remove(id);
                iDSelected = listIDs[0];
                UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
                ChangematFormID();
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
