using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public ButtonLevelCellUI buttonLevelCellUI;
    public List<ButtonLevelCellUI> buttonLevelCellUIs;
    public Transform tfButton;
    public RectTransform tfContent;
    private void Start()
    {
        Loadata();
    }
    public void ResetData()
    {
        for (int i = 0; i < buttonLevelCellUIs.Count; i++)
        {
            Destroy(buttonLevelCellUIs[i].gameObject);
        }
        buttonLevelCellUIs.Clear();
    }
    public void Loadata()
    {
        if (buttonLevelCellUIs.Count > 0)
        {
            ResetData();
        }
        if (buttonLevelCellUIs == null)
        {
            buttonLevelCellUIs = new List<ButtonLevelCellUI>();
        }
        for (int i = 0; i < LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels.Count; i++)
        {
            ButtonLevelCellUI btn = Instantiate(buttonLevelCellUI, tfButton);
            buttonLevelCellUIs.Add(btn);
            btn.LoadData(LevelManager.Ins.levelDataTFAssetData.levelDataTFDataModels[i].id);
        }
        StartCoroutine(IE_SetSizeDetal(LevelManager.Ins.indexLevel));
    }
    IEnumerator IE_SetSizeDetal(int targetButtonIndex)
    {
        yield return new WaitForEndOfFrame();
        if (tfContent != null && buttonLevelCellUIs.Count > 0)
        {
            RectTransform buttonRectTransform = buttonLevelCellUIs[0].GetComponent<RectTransform>();
            float buttonHeight = buttonRectTransform.rect.height + 100;
            float totalHeight = buttonHeight * buttonLevelCellUIs.Count;
            tfContent.sizeDelta = new Vector2(tfContent.sizeDelta.x, totalHeight);
            yield return new WaitForEndOfFrame();
            if (targetButtonIndex >= 0 && targetButtonIndex < buttonLevelCellUIs.Count)
            {
                RectTransform targetButtonRectTransform = buttonLevelCellUIs.Find(id => id.id == targetButtonIndex).GetComponent<RectTransform>();
                float targetButtonYPos = Mathf.Abs(targetButtonRectTransform.anchoredPosition.y + 100);
                float scrollPosition = (targetButtonYPos + buttonHeight / 2) / totalHeight;
                scrollPosition = 1 - scrollPosition;
                ScrollRect scrollRect = tfContent.GetComponentInParent<ScrollRect>();
                if(targetButtonIndex >= buttonLevelCellUIs.Count - 2)
                {
                    scrollRect.verticalNormalizedPosition = 1;
                }
                else
                {
                    scrollRect.verticalNormalizedPosition = scrollPosition;
                }
            }
        }
    }
}
