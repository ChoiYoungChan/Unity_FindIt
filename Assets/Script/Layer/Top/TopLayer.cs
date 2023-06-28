using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopLayer : BaseLayerTemplate
{
    [SerializeField] Button _startBtn, _soundBtn, _reviewBtn;
    [SerializeField] Sprite _soundOn, _soundOff;

    public virtual void Awake()
    {
        _startBtn.onClick.AddListener(OnClickStartButton);
        _soundBtn.onClick.AddListener(OnClickSoundButton);
        _reviewBtn.onClick.AddListener(OnClickReviewButton);
    }

    public virtual void OnEnable()
    {
        //SoundManager.Instance.Play("bgm");
        //if (GameManager.Instance.GetSoundOn()) {
        //    GameManager.Instance.SetSoundOn(false);
        //} else {
        //    GameManager.Instance.SetSoundOn(true);
        //}
        //_soundBtn.GetComponent<Image>().sprite = GameManager.Instance.GetSoundOn() ? _soundOn : _soundOff;
    }

    private void OnClickStartButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    private void OnClickSoundButton()
    {
        //if (GameManager.Instance.GetSoundOn()) {
        //    GameManager.Instance.SetSoundOn(false);
        //} else {
        //    GameManager.Instance.SetSoundOn(true);
        //}

        //_soundBtn.GetComponent<Image>().sprite = GameManager.Instance.GetSoundOn() ? _soundOn : _soundOff;
        //SoundManager.Instance.Play("bgm");
    }

    private void OnClickReviewButton()
    {
        Application.OpenURL("");
    }
}
