using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonLevelCellUI : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI text;
    public void LoadData(int _id)
    {
        id = _id;
        text.text = (id + 1).ToString();
    }
    public void btnIndexLevel()
    {
        SceneController.Ins.ChangeScene("GamePlay", () =>
        {
            UIManager.Ins.CloseUI<Home>();
            UIManager.Ins.OpenUI<GamePlay>();
            UIManager.Ins.bg.gameObject.SetActive(false);
            LevelManager.Ins.indexIDLV = id;
           
        }, true, true);
    }
}
