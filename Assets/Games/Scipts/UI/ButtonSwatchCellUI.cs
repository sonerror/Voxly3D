using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwatchCellUI : MonoBehaviour
{
    public ButtonSwatch buttonSwatch;
    public Transform tf;
    public List<ButtonSwatch> buttonSwatches;
    public void LoadData()
    {
        if (buttonSwatches.Count > 0)
        {
            foreach (var buttonSwatch in buttonSwatches)
            {
                Destroy(buttonSwatch.gameObject);
            }
            buttonSwatches.Clear();
        }
        for (int i = 0; i < LevelManager.Ins.listID.Count; i++)
        {
            ButtonSwatch btn = Instantiate(buttonSwatch, tf);
            btn.id = LevelManager.Ins.listID[i];
            buttonSwatches.Add(btn);
            btn.SelectButton();
        }
    }


}
