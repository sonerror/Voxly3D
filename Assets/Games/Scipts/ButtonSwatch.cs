using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwatch : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI textID;
    public Image imgBG;
    ButtonSwatchCellUI buttonSwatchCellUI;
    private RectTransform rectTransform;
    public Vector2 initialPosition;
    public Color color;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(IE_Loadata());

    }
    public void Start()
    {
        StartCoroutine(IE_Loadata());
        textID.text = id.ToString();
    }
    IEnumerator IE_Loadata()
    {
        yield return new WaitForEndOfFrame();
        initialPosition = rectTransform.anchoredPosition;
    }
    public void Onint(ButtonSwatchCellUI buttonSwatchCellUI)
    {
        this.buttonSwatchCellUI = buttonSwatchCellUI;
        StartCoroutine(IE_Loadata());
    }

    public void btnID()
    {
        LevelManager.Ins.CheckID(id);
        LevelManager.Ins.ChangematFormID();
        buttonSwatchCellUI.SelectButton();
    }
    public void SetBG(Color color)
    {
        imgBG.color = color;
    }
    public void MoveUp()
    {
        rectTransform.DOAnchorPos(new Vector2(initialPosition.x, initialPosition.y + 40f), 0.3f);
    }

    public void MoveDown()
    {
        rectTransform.DOAnchorPos(initialPosition, 0.5f);
    }
}