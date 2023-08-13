using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : BaseLayerTemplate
{
    [SerializeField] Button NextBtn, BackBtn;
    [SerializeField] Image ResultImg;
    [SerializeField] Sprite[] ResultSprite;
    [SerializeField] Sprite[] ButtonSprite;

    private bool b_canShake;
    private float _shakeTimer = 3;

    public override void Awake()
    {
        NextBtn.onClick.AddListener(OnClickNextButton);
        BackBtn.onClick.AddListener(OnClickBackButton);
    }

    public override void OnEnable()
    {
        // initialize variable and object when OnEnable
        b_canShake = false;
        NextBtn.gameObject.SetActive(false);
        _shakeTimer = 3;

        // play sound according clear trigger
        if (GameManager.Instance.GetIsClear()) {
            SoundManager.Instance.Play("clear");
        } else {
            SoundManager.Instance.Play("miss");
        }

        ShowResultImage();
        ShowResultEffect();
    }

    private void Update()
    {
        if (b_canShake) {
            StartShakeEffect();
        }
    }

    /// <summary>
    /// onclick action when next button is pressed
    /// </summary>
    private void OnClickNextButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    /// <summary>
    /// onclick action when back button is pressed
    /// </summary>
    private void OnClickBackButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    /// <summary>
    /// show result image according result trigger
    /// </summary>
    private void ShowResultImage()
    {
        ResultImg.sprite = GameManager.Instance.GetIsClear() ? ResultSprite[0] : ResultSprite[1];
        NextBtn.GetComponent<Image>().sprite = GameManager.Instance.GetIsClear() ? ButtonSprite[0] : ButtonSprite[1];
    }

    /// <summary>
    /// show result effect according result trigger
    /// </summary>
    private void ShowResultEffect()
    {
        if(!GameManager.Instance.GetIsClear()) b_canShake = true;
        DelayControlClass.Instance.CallAfter(1.0f, ()=> { NextBtn.gameObject.SetActive(true); });
    }

    /// <summary>
    /// image shake effect for clear
    /// </summary>
    public void StartShakeEffect()
    {
        if (_shakeTimer > 0) {
            ResultImg.transform.localPosition = Vector3.zero + UnityEngine.Random.insideUnitSphere * 4.0f;
            _shakeTimer -= Time.deltaTime;
        } else {
            _shakeTimer = 0f;
            ResultImg.transform.localPosition = Vector3.zero;
            b_canShake = false;
        }
    }
}
