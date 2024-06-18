using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwatchCellUI : MonoBehaviour
{
    public ButtonSwatch buttonSwatch;
    public Transform tf;
    public List<ButtonSwatch> buttonSwatches;
    public RectTransform tfContent;
    private void Awake()
    {

    }
    public void LoadData()
    {
        DestroyButton();
        for (int i = 0; i < LevelManager.Ins.listIDs.Count; i++)
        {
            ButtonSwatch btn = Instantiate(buttonSwatch, tf);
            btn.id = LevelManager.Ins.listIDs[i];
            buttonSwatches.Add(btn);
        }
        SetSizeDetal();
    }
    public void DestroyButton()
    {
        if (buttonSwatches.Count > 0)
        {
            foreach (var buttonSwatch in buttonSwatches)
            {
                Destroy(buttonSwatch.gameObject);
            }
            buttonSwatches.Clear();
        }
    }
    public void SetSizeDetal()
    {
        if (tfContent != null && buttonSwatches.Count > 0)
        {
            RectTransform buttonRectTransform = buttonSwatches[0].GetComponent<RectTransform>();
            float buttonWidth = buttonRectTransform.rect.width;
            float totalWidth = buttonWidth * buttonSwatches.Count;

            tfContent.sizeDelta = new Vector2(totalWidth, tfContent.sizeDelta.y);
        }
    }


   
    public void btnID(int id)
    {
        LevelManager.Ins.CheckID(id);
        LevelManager.Ins.ChangematFormID();
    }

    public void SelectButton()
    {
        for (int i = 0; i < buttonSwatches.Count; i++)
        {
            if (buttonSwatches[i].id == LevelManager.Ins.iDSelected)
            {
                buttonSwatches[i].SetBG(Color.blue);
            }
            else
            {
                buttonSwatches[i].SetBG(Color.white);

            }
        }
    }

}
