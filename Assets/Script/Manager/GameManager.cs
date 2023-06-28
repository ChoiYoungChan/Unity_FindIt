using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    public static bool _isSoundOn, _isClear;
    public static int _hintCount;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        InitializeData();
    }

    private void InitializeData()
    {
        _isSoundOn = true;
        _isClear = false;
        _hintCount = 0;
        SetSoundOn(_isSoundOn);
    }

    private void Start()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    public void SetSoundOn(bool _isActive)
    {
        _isSoundOn = _isActive;
        CacheData.Instance.SoundOn = _isSoundOn;
    }

    public bool GetSoundOn()
    {
        return _isSoundOn;
    }

    public void SetIsClear(bool _isActive)
    {
        _isClear = _isActive;
    }

    public bool GetIsClear()
    {
        return _isClear;
    }

    public void SetHintCount(int _count)
    {
        _hintCount = _count;
        CacheData.Instance.HintCount = _hintCount;
    }

    public int GetHintCount()
    {
        return _hintCount;
    }
}
