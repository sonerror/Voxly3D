using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevelCellUI : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI text;
    public Image sprite;
   
    public void LoadData(int _id)
    {
        id = _id;
        text.text = (id + 1).ToString();
        sprite.sprite = LevelManager.Ins.levelDataTFAssetData.GetLevelDataWithID(id).sprite;
    }
    public void btnIndexLevel()
    {
        LevelManager.Ins.indexLevel = id;
        if(SceneController.Ins.currentSceneName.Equals("GamePlay"))
        {
            UIManager.Ins.CloseUI<Home>();
            UIManager.Ins.OpenUI<GamePlay>();
            UIManager.Ins.bg.gameObject.SetActive(false);
            LevelManager.Ins.LoadLevel(id);
        }
    }
}
