using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels;
    public Level levelCurrent;
    public GameObject voxelPiece;
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
    }
    public void LoadLevel()
    {
        DestroyCurrentLevel();
        InstantiateLevel();
        levelCurrent.gameObject.SetActive(true);
        levelCurrent.Onint();
    }
    public void InstantiateLevel()
    {
        levelCurrent = Instantiate(levels[0]);
    }
    public void DestroyCurrentLevel()
    {
        if (levelCurrent) Destroy(levelCurrent.gameObject);
    }
}