using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

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
  
    public void NextLevel()
    {
        indexLevel++;
        iDSelected = 0;
        LoadLevel(indexLevel);
    }
    public void InstantiateLevel(int indexLevel)
    {
        levelCurrent = Instantiate(_levelIndex);
        LevelData levelDataTF = levelDataTFAssetData.GetLevelDataWithID(indexLevel).levelDatas;
        MatManager.Ins.listID = new List<MaterialData>(levelDataTF.materials);
        if(levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount <= 0 )
        {
            levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount = 120;
        }
        levelCurrent.countDownTime = levelDataTFAssetData.GetLevelDataWithID(indexLevel).timeCount;
        MatManager.Ins.listIDBtn = new List<MaterialData>(levelDataTF.materials);
        foreach (TransformData tf in levelDataTF.tfData)
        {
            VoxelPiece v = SimplePool.Spawn<VoxelPiece>(PoolType.VoxelPiece);
            v.ID = tf.realColorID;
            v.Oninit();
            v.TF.SetParent(null);
            v.TF.position = tf.position;
            v.TF.SetParent(levelCurrent.container);
            if (indexLevel == 0)
            {
                v.TF.localScale = new Vector3(10, 10, 10);
            }
        }
        levelCurrent.transform.localRotation = Quaternion.Euler(10, 120, -20);
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
            ButtonSwatch buttonSwatch = UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.buttonSwatches.Find(_i => _i.id == id);
            buttonSwatch.SetIMGType(levelCurrent.FCountVoxel(buttonSwatch.id));
            if (levelCurrent.CountVoxel(id) <= 0)
            {
                StartCoroutine(IE_ResetUI(id));
            }
        }
        else
        {
            ButtonSwatch buttonSwatch = UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.buttonSwatches.Find(_i => _i.id == id);
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
        if (SceneController.Ins.isLoadingNewScene) return;
        if (!levelCurrent.isCountDown) return;
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
        yield return new WaitForEndOfFrame();
        iDSelected = MatManager.Ins.listIDBtn[0].colorID;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        UIManager.Ins.OpenUI<GamePlay>().buttonSwatchCellUI.LoadData();
        yield return new WaitForEndOfFrame();
        ChangematFormID();
    }
    public void SetMatZoomIn()// phong to obj
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            if (levelCurrent.voxelPieces[i].isVoxel != true)
            {
                MatManager.Ins.ChangeMatNumber(levelCurrent.voxelPieces[i], levelCurrent.voxelPieces[i].ID);
            }
        }
    }
    public void SetMatZoomOut()//thu nho obj
    {
        for (int i = 0; i < levelCurrent.quantity; i++)
        {
            MatManager.Ins.ChangeMat(levelCurrent.voxelPieces[i]);
            if (levelCurrent.voxelPieces[i].isVoxel != true)
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
        if(UIManager.Ins.IsOpened<GamePlay>())
        {
            UIManager.Ins.OpenUI<GamePlay>().ToggleButtons(1, 0);
        }
    }
    public void SetBtnZoomOut()
    {
        if(UIManager.Ins.IsOpened<GamePlay>())
        {
            UIManager.Ins.OpenUI<GamePlay>().ToggleButtons(0, 1);
        }
    }
}
