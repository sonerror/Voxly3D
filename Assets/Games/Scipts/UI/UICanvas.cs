using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : RectUnit
{
    public bool IsDestroyOnClose = false;
    public bool isAllButtonEffect = true;


    public virtual void Setup()
    {
        UIManager.Ins.AddBackUI(this);
        UIManager.Ins.PushBackAction(this, BackKey);
    }


    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseDirectly()
    {
        UIManager.Ins.RemoveBackUI(this);
        gameObject.SetActive(false);
        if (IsDestroyOnClose)
        {
            Destroy(gameObject);
        }
        
    }

    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

}
