using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel : MonoBehaviour
{
    public ButtonLevelCellUI buttonLevelCellUI;
    public List<ButtonLevelCellUI> buttonLevelCellUIs;
    public Transform tfButton;
    private void Start()
    {
        Loadata();
    }
    public void Loadata()
    {
        for (int i = 0; i < LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels.Count; i++)
        {
            ButtonLevelCellUI btn = Instantiate(buttonLevelCellUI, tfButton);
            buttonLevelCellUIs.Add(btn);
        }
    }
    public void LoadataCell(int id)
    {
        for (int i = 0; i < LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels.Count; i++)
        {
            id = LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels[i].id;
        }
    }
}
