using UnityEngine;
using System.Collections;

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
#if UNITY_ANDROID
        if (isAndroid())
            vibrator.Call("vibrate");
#if UNITY_ANDROID
        else
            Handheld.Vibrate();
#endif
    }


    public static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
#if UNITY_ANDROID
        else
            Handheld.Vibrate();
#endif
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
#if UNITY_ANDROID
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
#if UNITY_ANDROID
        else
            Handheld.Vibrate();
#endif
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
#if UNITY_ANDROID
        if (isAndroid())
            vibrator.Call("cancel");
#endif
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	    return true;
#else
        return false;
#endif
    }
}
