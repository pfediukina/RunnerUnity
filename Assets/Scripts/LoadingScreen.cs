using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance {get; private set;}
    public static bool IsNull => Instance == null ? true : false;

    public static Action<bool> OnLoadingShown;

    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
        OnLoadingShown = null;
        Instance = this;
    }

    public static void ShowWindow(bool show)
    {
        if(LoadingScreen.IsNull) return;

        Instance._canvas.alpha = show ? 1 : 0;
        Instance._canvas.blocksRaycasts = show;
        OnLoadingShown?.Invoke(show);
    }
}
