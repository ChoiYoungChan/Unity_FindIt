using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : BaseDialogTemplate
{
    [SerializeField] Button _soundBtn, _shareBtn, _closeBtn;
    [SerializeField] Sprite _soundOn, _soundOff;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {
        _soundBtn.onClick.AddListener(OnClickSoundBtn);
        _shareBtn.onClick.AddListener(OnClickShareBtn);
        _closeBtn.onClick.AddListener(OnClickCloseBtn);
    }

    public virtual void UpdateData() { }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }


    private void OnClickSoundBtn()
    {
        if (GameManager.Instance.GetSoundOn()) {
            GameManager.Instance.SetSoundOn(false);
            _soundBtn.GetComponent<Image>().sprite = _soundOff;
        } else {
            GameManager.Instance.SetSoundOn(true);
            _soundBtn.GetComponent<Image>().sprite = _soundOn;
        }
        SoundManager.Instance.Play("bgm");
    }

    private void OnClickShareBtn()
    {

    }
    
    private void OnClickCloseBtn()
    {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }
}

