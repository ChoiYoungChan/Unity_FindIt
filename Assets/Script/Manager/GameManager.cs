using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    private static bool _isSoundOn, _isClear;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        InitializeData();
    }

    /// <summary>
    /// 
    /// </summary>
    private void InitializeData()
    {
        _isSoundOn = true;
        _isClear = false;
        SetSoundOn(_isSoundOn);
    }

    private void Start()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    /// <summary>
    /// set sound is on or off
    /// </summary>
    /// <param name="_isActive"></param>
    public void SetSoundOn(bool _isActive)
    {
        _isSoundOn = _isActive;
        CacheData.Instance.SoundOn = _isSoundOn;
    }

    /// <summary>
    /// get bool value about sound is on or off
    /// </summary>
    /// <returns></returns>
    public bool GetSoundOn()
    {
        return CacheData.Instance.SoundOn;
    }

    /// <summary>
    /// set bool clear variable value
    /// </summary>
    /// <param name="_isActive"></param>
    public void SetIsClear(bool _isActive)
    {
        _isClear = _isActive;
    }

    /// <summary>
    /// get clear bool value
    /// </summary>
    /// <returns></returns>
    public bool GetIsClear()
    {
        return _isClear;
    }

}
