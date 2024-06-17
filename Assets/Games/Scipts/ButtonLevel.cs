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
        if(buttonLevelCellUIs == null)
        {
            buttonLevelCellUIs = new List<ButtonLevelCellUI>();
        }
        for (int i = 0; i < LevelManager.Ins.levelDataAssetData.dataLevels.Count; i++)
        {
            ButtonLevelCellUI btn = Instantiate(buttonLevelCellUI, tfButton);
            buttonLevelCellUIs.Add(btn);
            btn.LoadData(LevelManager.Ins.levelDataAssetData.dataLevels[i].id);
        }
    }
    public void LoadataCell(int id)
    {
        for (int i = 0; i < LevelManager.Ins.levelDataAssetData.dataLevels.Count; i++)
        {
            id = LevelManager.Ins.levelDataAssetData.dataLevels[i].id;
        }
    }
}
