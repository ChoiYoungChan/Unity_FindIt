using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheData : SingletonClass<CacheData>
{
    public enum CacheDataKey {
        FirstOpen,
        HintCount,
        SoundOn,
        Tutorial,
        StartCountdown
    }

    public string GetKey(CacheDataKey keys) { return keys.ToString(); }

    public bool IsFirstOpen
    {
        get { return !GetValue<bool>(GetKey(CacheDataKey.FirstOpen)); }
        set { SetValue(GetKey(CacheDataKey.FirstOpen), !value); }
    }

    public int HintCount
    {
        get { return GetValue<int>(GetKey(CacheDataKey.HintCount)); }
        set { SetValue(GetKey(CacheDataKey.HintCount), value); }
    }

    public bool SoundOn
    {
        get { return !GetValue<bool>(GetKey(CacheDataKey.SoundOn)); }
        set { SetValue(GetKey(CacheDataKey.SoundOn), !value); }
    }

    public bool Tutorial
    {
        get { return !GetValue<bool>(GetKey(CacheDataKey.Tutorial)); }
        set { SetValue(GetKey(CacheDataKey.Tutorial), !value); }
    }

    public bool StartCountdown
    {
        get { return !GetValue<bool>(GetKey(CacheDataKey.StartCountdown)); }
        set { SetValue(GetKey(CacheDataKey.StartCountdown), !value); }
    }

    public T GetValue<T>(string key)
    {
        Type type = typeof(T);

        if (type == typeof(int)) return (T)(object)PlayerPrefs.GetInt(key);
        if (type == typeof(float)) return (T)(object)PlayerPrefs.GetFloat(key);
        if (type == typeof(string)) return (T)(object)PlayerPrefs.GetString(key);
        if (type == typeof(bool)) return (T)(object)Convert.ToBoolean(PlayerPrefs.GetInt(key));

        throw new System.Exception(type + " is not suported");
    }

    public void SetValue<T>(String key, T value)
    {
        Type type = typeof(T);

        if (type == typeof(int)) PlayerPrefs.SetInt(key, (int)(object)value);
        if (type == typeof(float)) PlayerPrefs.SetFloat(key, (float)(object)value);
        if (type == typeof(string)) PlayerPrefs.SetString(key, (string)(object)value);
        if (type == typeof(bool)) PlayerPrefs.SetInt(key, Convert.ToInt32((bool)(object)value));

        PlayerPrefs.Save();
    }
}
