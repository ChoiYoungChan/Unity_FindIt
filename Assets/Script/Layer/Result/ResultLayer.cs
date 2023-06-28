using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : BaseLayerTemplate
{
    [SerializeField] Button _nextBtn, _backBtn;
    [SerializeField] Image _resultImg;
    [SerializeField] Sprite[] _resultSprite;

    private bool canShake;
    private float _shakeTimer = 3;

    public virtual void Awake()
    {
        _nextBtn.onClick.AddListener(OnClickNextButton);
        _backBtn.onClick.AddListener(OnClickBackButton);
    }

    public virtual void OnEnable()
    {
        canShake = false;
        _nextBtn.gameObject.SetActive(false);
        _shakeTimer = 3;

        if (GameManager.Instance.GetIsClear())
        {
            SoundManager.Instance.Play("clear");
        } else {
            SoundManager.Instance.Play("miss");
        }

        ShowResultImage();
        ShowResultEffect();
    }

    private void Update()
    {
        if (canShake) {
            StartShakeEffect();
        }
    }

    private void OnClickNextButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }

    private void OnClickBackButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
    }

    private void ShowResultImage()
    {
        //_resultSprite[0] = clearImage, _resultSprite[1] = failureImage
        _resultImg.sprite = GameManager.Instance.GetIsClear() ? _resultSprite[0] : _resultSprite[1];
    }

    private void ShowResultEffect()
    {
        if(!GameManager.Instance.GetIsClear()) canShake = true;
        DelayControlClass.Instance.CallAfter(1.0f, ()=> { _nextBtn.gameObject.SetActive(true); });
    }

    public void StartShakeEffect()
    {
        if (_shakeTimer > 0) {
            _resultImg.transform.localPosition = Vector3.zero + UnityEngine.Random.insideUnitSphere * 4.0f;
            _shakeTimer -= Time.deltaTime;
        } else {
            _shakeTimer = 0f;
            _resultImg.transform.localPosition = Vector3.zero;
            canShake = false;
        }
    }
}
