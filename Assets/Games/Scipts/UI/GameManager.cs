using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.VFX;

public class GameManager : Singleton<GameManager>
{
    private bool isInitGame;
    protected void Awake()
    {
        Input.multiTouchEnabled = true;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    private void Start()
    {
        StartCoroutine(I_InitGame());
    }
    IEnumerator I_InitGame()
    {
        yield return new WaitUntil(
            () => (
            Ins != null
           
            && UIManager.Ins != null
            && LevelManager.Ins != null
            )
        );
        UIManager.Ins.OpenUI<Loading>();
        UIManager.Ins.bg.SetActive(false);

        yield return new WaitForEndOfFrame();
        isInitGame = true;
        yield return null;
    }
}
