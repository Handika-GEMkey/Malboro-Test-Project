using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebGLBridger : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OnSubmitScore(int score);

    [DllImport("__Internal")]
    private static extern void OnExit();

    [DllImport("__Internal")]
    private static extern void OnPlay();

    [DllImport("__Internal")]
    private static extern int OnGetPoint();

    [DllImport("__Internal")]
    private static extern int OnGetCar();

    public void SubmitScore(int score)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    OnSubmitScore(score);
#endif
    }

    public void Exit()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    OnExit();
#endif
    }

    public void Play()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
    OnPlay();
#endif
    }

    public int GetPoint()
    {
        return OnGetPoint();
    }

    public int GetCar()
    {
        return OnGetCar();
    }
}
