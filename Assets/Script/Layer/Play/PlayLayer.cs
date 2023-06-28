using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayLayer : BaseLayerTemplate
{
    [SerializeField] Button _settingBtn, _backBtn, _hintBtn;
    [SerializeField] GameObject _boxObject;
    [SerializeField] GameObject _tutorial, _mapObject, _cover;
    [SerializeField] Transform _firstPos;
    [SerializeField] Image _countdownImg;
    [SerializeField] Sprite[] _countdownSprite;

    private List<GameObject> _boxList = new List<GameObject>();
    private List<Vector2> _wavePosList = new List<Vector2>();
    private int _countDownTime;
    private bool _canTouch;

    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {
        if (CacheData.Instance.IsFirstOpen) {
            CacheData.Instance.Tutorial = false;
            _tutorial.SetActive(true);
            CacheData.Instance.IsFirstOpen = false;
        }
        _wavePosList.Add(_cover.transform.position);
        _wavePosList.Add(new Vector2(_cover.transform.position.x - 5000, _cover.transform.position.y));

        _settingBtn.onClick.AddListener(()=> { DialogManager.Instance.OpenDialog(DialogManager.DialogKey.DialogKey_Setting);});
        _backBtn.onClick.AddListener(() => {
            _mapObject.gameObject.SetActive(false);
            LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
        });
    }

    public virtual void OnEnable()
    {
        SoundManager.Instance.Play("bgm");
        _countDownTime = 3;
        _countdownImg.gameObject.SetActive(false);
        CacheData.Instance.StartCountdown = true;
        _mapObject.gameObject.SetActive(true);
        _canTouch = false;

        if (_boxList.Count < 1) {
            GeterateObject();
        } else {
            for(int count = 0; count < _boxList.Count; count++) {
                _boxList[count].gameObject.GetComponent<BoxObject>().Initialize();
            }
            var randomNum = UnityEngine.Random.Range(0, (_boxList.Count - 1));
            _boxList[randomNum].GetComponent<BoxObject>().SetIsAnswer(true);
        }
    }

    private void Update()
    {
        if(CacheData.Instance.Tutorial && CacheData.Instance.StartCountdown) {
            CacheData.Instance.StartCountdown = false;
            StartCoroutine("Timer");
        }

        if (_canTouch && Input.GetMouseButton(0)) {
            _canTouch = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider.tag == "box") {
                hit.transform.gameObject.GetComponent<BoxObject>().OnAction(MoveLayer);
            }
        }
    }

    IEnumerator Timer()
    {
        while(_countDownTime <= 3 && _countDownTime >= 0) {
            CountDown(_countDownTime);
            _countDownTime--;
            yield return new WaitForSeconds(1f);
            if (_countDownTime < 0) {
                _countDownTime = 0;
                // flag out anime
                _canTouch = true;
                break;
            }
        }
    }

    private void CountDown(int _time)
    {
        // flag in anime
        _cover.transform.position = Vector2.Lerp(_cover.transform.position, _wavePosList[0], 2.0f);
        _countdownImg.gameObject.SetActive(true);
        _countdownImg.sprite = _countdownSprite[_time];
        if (_time == 0) {
            _cover.transform.position = Vector2.Lerp(_cover.transform.position, _wavePosList[1], 2.0f);
            _countdownImg.gameObject.SetActive(false);
        }
    }

    private void GeterateObject()
    {
        Vector3 position = _firstPos.transform.position;
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(_boxObject, position, _boxObject.transform.rotation);
            obj.GetComponent<BoxObject>().Initialize();
            position.x += 3;
            position.z -= 3;
            _boxList.Add(obj);
        }
        var randomNum = UnityEngine.Random.Range(0, (_boxList.Count - 1));
        _boxList[randomNum].GetComponent<BoxObject>().SetIsAnswer(true);
        _boxObject.SetActive(false);
    }

    /// <summary>
    /// MoveNextlayer
    /// </summary>
    public void MoveLayer()
    {
        CacheData.Instance.StartCountdown = false;
        _mapObject.gameObject.SetActive(false);
    }
}
