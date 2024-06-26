using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelDataTFAssetData levelDataTFAssetData;
    public Level _levelIndex;
    public Level levelCurrent;
    public GameObject voxelPiece;
    public int iDSelected;
    public bool areDrawing = false;
    public int indexLevel = 0;
    public bool isChangeMatSl = false;
    public bool isWin = false;
    public bool isUsingBooster = false;
    public bool isInstantiateLevel = false;

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
        if (SceneController.Ins != null && SceneController.Ins.currentSceneName.Equals("GamePlay") && !UIManager.Ins.IsOpened<Home>())
        {
            LoadLevel(indexLevel);
        }
    }

    public void LoadLevel(int _indelLevel)
    {
        DestroyCurrentLevel();
        InstantiateLevel(_indelLevel);
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
        StartCoroutine(IE_LoadCellButtonSwatch());
        isChangeMatSl = false;
        isWin = false;
        isUsingBooster = false;
    }
    public void ResetData()
    {
        DestroyCurrentLevel();
        isChangeMatSl = false;
        isWin = false;
        isUsingBooster = false;
        //levelCurrent.gameObject.SetActive(true);

    }
    IEnumerator IE_LoadCellButtonSwatch()
    {
        if (!UIManager.Ins.IsOpened<Home>())
        {
            yield return new WaitForSeconds(0.1f);
            UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
        }
    }

    public void NextLevel()
    {
        indexLevel++;
        if (indexLevel < levelDataTFAssetData.levelDataTFDataModels.Count - 1)
        {
            iDSelected = 0;
            LoadLevel(indexLevel);
        }
        else
        {
            indexLevel = 0;
            iDSelected = 0;
            LoadLevel(indexLevel);
        }
            
    }
    public void InstantiateLevel(int indexLevel)
    {
        levelCurrent = Instantiate(_levelIndex);
        LevelData originalLevelData = levelDataTFAssetData.GetLevelDataWithID(indexLevel).levelDatas;
        LevelData levelDataTF = originalLevelData.DeepCopy();
        MatManager.Ins.listID = new List<MaterialData>(levelDataTF.materials);
        if (levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount <= 0)
        {
            levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount = 120;
        }
        levelCurrent.countDownTime = levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount;
        StartCoroutine(IE_LoadValZoom(levelCurrent, levelDataTF));
        MatManager.Ins.listIDBtn = new List<MaterialData>(levelDataTF.materials);
        foreach (TransformData tf in levelDataTF.tfData)
        {
            VoxelPiece v = SimplePool.Spawn<VoxelPiece>(PoolType.VoxelPiece);
            v.ID = tf.realColorID;
            v.Oninit();
            v.TF.SetParent(null);
            v.TF.position = tf.position;
            v.TF.localRotation = Quaternion.Euler(0, 0, 0);
            v.TF.SetParent(levelCurrent.container);
        }
        levelCurrent.transform.localRotation = Quaternion.Euler(24, -120, 20);
       
    }
    IEnumerator IE_LoadValZoom(Level lv, LevelData lvdata)
    {
        yield return new WaitForEndOfFrame();
        lv.zoomAndMoveLevel.minOrthoSize = lvdata.minOrthoSize;
        lv.zoomAndMoveLevel.maxOrthoSize = lvdata.maxOrthoSize;
        yield return new WaitForEndOfFrame();
        lv.zoomAndMoveLevel.Onint();
    }
    public void DestroyCurrentLevel()
    {
        if (levelCurrent != null)
        {
            iDSelected = 0;
            Destroy(levelCurrent.gameObject);
            levelCurrent = null;
        }
    }

    public void ChangematFormID()
    {
        if (isChangeMatSl)
        {
            for (int i = 0; i < levelCurrent.quantity; i++)
            {
                if (!levelCurrent.voxelPieces[i].isVoxel)
                {
                    if (levelCurrent.voxelPieces[i].ID == iDSelected)
                    {
                        levelCurrent.voxelPieces[i].mesh.material.color = new Color32(54, 69, 79, 255);
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
            ButtonSwatch buttonSwatch = UIManager.Ins.GetUI<GamePlay>().buttonSwatchCellUI.buttonSwatches.Find(_i => _i.id == id);
            buttonSwatch.SetIMGType(levelCurrent.FCountVoxel(buttonSwatch.id));
            if (levelCurrent.CountVoxel(id) <= 0)
            {
                StartCoroutine(IE_ResetUI(id));
            }
        }
        else
        {
            ButtonSwatch buttonSwatch = UIManager.Ins.GetUI<GamePlay>().buttonSwatchCellUI.buttonSwatches.Find(_i => _i.id == id);
            buttonSwatch.SetIMGType(levelCurrent.FCountVoxel(buttonSwatch.id));
            isWin = true;
            levelCurrent.StopCountDown();
            levelCurrent.ChangeAnim();
            DOVirtual.DelayedCall(3f, () =>
            {
                UIManager.Ins.CloseAll();
                UIManager.Ins.OpenUI<Win>();
            });
        }
    }

    public void CheckWinloseTimer()
    {
        if (SceneController.Ins == null || SceneController.Ins.isLoadingNewScene || !levelCurrent.isCountDown)
        {
            return;
        }

        if (levelCurrent.countDownTime <= 0)
        {
            isWin = true;
            levelCurrent.StopCountDown();
            UIManager.Ins.OpenUI<Lose>();
        }
    }

    IEnumerator IE_ResetUI(int id)
    {
        yield return new WaitForEndOfFrame();
        MatManager.Ins.listIDBtn.RemoveAll(i => i.colorID == id);
        yield return new WaitForEndOfFrame();
        iDSelected = MatManager.Ins.listIDBtn[0].colorID;
        yield return new WaitForEndOfFrame();
        UIManager.Ins.GetUI<GamePlay>().buttonSwatchCellUI.LoadData();
        yield return new WaitForEndOfFrame();
        ChangematFormID();
    }

    public void SetMatZoomIn()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (!levelCurrent.voxelPieces[i].isVoxel)
            {
                MatManager.Ins.ChangeMatNumber(levelCurrent.voxelPieces[i], levelCurrent.voxelPieces[i].ID);
            }
        }
    }

    public void SetMatZoomOut()
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            MatManager.Ins.ChangeMat(levelCurrent.voxelPieces[i]);
            if (!levelCurrent.voxelPieces[i].isVoxel)
            {
                ChangeMatCurrent(levelCurrent.voxelPieces[i]);
            }
        }
    }

    public void SetMatDef(VoxelPiece voxelPiece)
    {
    }

    public void ChangeMatCurrent(VoxelPiece voxelPiece)
    {
        MatManager.Ins.ChangeMatCurent(voxelPiece);
    }

    public void SetBtnZoomIn()
    {
        if (UIManager.Ins.IsOpened<GamePlay>())
        {
            UIManager.Ins.GetUI<GamePlay>().ToggleButtons(1, 0);
        }
    }

    public void SetBtnZoomOut()
    {
        if (UIManager.Ins.IsOpened<GamePlay>())
        {
            UIManager.Ins.GetUI<GamePlay>().ToggleButtons(0, 1);
        }
    }

    public void FLTarget()
    {
        UIManager.Ins.GetUI<GamePlay>().ToggleButtons(0, 1);
        levelCurrent.ZoomInLevel();
    }
}
