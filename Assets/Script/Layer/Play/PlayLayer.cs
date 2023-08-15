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
    private GameObject _chestObject;
    private int _countDownTime;
    private bool _canTouch;

    public override void Awake()
    {
        Debug.Log("### Awake");
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public override void Initialize()
    {
        Debug.Log("### Initialize");
        _chestObject = _boxObject;
        // show tutorial dialog first open only
        if (CacheData.Instance.IsFirstOpen) {
            CacheData.Instance.Tutorial = false;
            _tutorial.SetActive(true);
            GenerateObject();
            CacheData.Instance.IsFirstOpen = false;
        }
        //
        _wavePosList.Add(_cover.transform.position);
        _wavePosList.Add(new Vector2(_cover.transform.position.x - 5000, _cover.transform.position.y));

        _settingBtn.onClick.AddListener(()=> { DialogManager.Instance.OpenDialog(DialogManager.DialogKey.DialogKey_Setting);});
        _backBtn.onClick.AddListener(() => {
            _mapObject.gameObject.SetActive(false);
            LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Top);
        });
    }

    public override void OnEnable()
    {
        Debug.Log("### OnEnable");
        SoundManager.Instance.Play("bgm");
        CacheData.Instance.StartCountdown = true;

        // initialize countdown valuable and object when OnEnable
        _countDownTime = 3;
        _countdownImg.gameObject.SetActive(false);
        _mapObject.gameObject.SetActive(true);
        _canTouch = false;

        int _randomObjectNumber = RandomObjectNumber();
        SetBox(_randomObjectNumber);

        // 
        /*if (_boxList.Count < 1){
            GeterateObject();
        } else {
            for(int count = 0; count < _boxList.Count; count++) {
                _boxList[count].gameObject.GetComponent<BoxObject>().Initialize();
            }
            var randomNum = UnityEngine.Random.Range(0, (_boxList.Count - 1));
            _boxList[randomNum].GetComponent<BoxObject>().SetIsAnswer(true);
        }*/
    }

    /// <summary>
    /// start count down and when treasure chest is choosed
    /// </summary>
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
                hit.transform.gameObject.GetComponent<BoxObject>().SetAction(MoveLayer);
            }
        }
    }

    /// <summary>
    /// Move to Next layer
    /// </summary>
    public void MoveLayer()
    {
        CacheData.Instance.StartCountdown = false;
        _mapObject.gameObject.SetActive(false);
        ActiveFalseAllBox();
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

    /// <summary>
    /// count down timer
    /// </summary>
    /// <param name="_time"></param>
    private void CountDown(int _time)
    {
        // flag in anime
        _cover.transform.position = Vector2.Lerp(_cover.transform.position, _wavePosList[0], 2.0f);
        _countdownImg.gameObject.SetActive(true);
        _countdownImg.sprite = _countdownSprite[_time];
        // when count down is done
        if (_time == 0) {
            _cover.transform.position = Vector2.Lerp(_cover.transform.position, _wavePosList[1], 2.0f);
            _countdownImg.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// generate objects and init setting when generate
    /// </summary>
    private void GenerateObject()
    {
        Vector3 _position = _firstPos.transform.position;
        for (int count = 0; count < 10; count++)
        {
            GameObject obj = Instantiate(_chestObject, _position, _chestObject.transform.rotation);
            obj.SetActive(false);
            _boxList.Add(obj);
        }
        Debug.Log("### BoxList count : " + _boxList.Count);
        _boxObject.SetActive(false);
    }

    /// <summary>
    /// set box position and answer
    /// </summary>
    /// <param name="randomNum"></param>
    private void SetBox(int randomNum)
    {
        if (_boxList.Count == null || _boxList.Count < 2) GenerateObject();

        Vector3 _position = _firstPos.transform.position;
        Debug.Log("### random number : " + randomNum);
        for (int count = 0; count < randomNum; count++)
        {
            _boxList[count].gameObject.SetActive(true);
            _boxList[count].gameObject.GetComponent<BoxObject>().Initialize();
            _boxList[count].gameObject.transform.position = new Vector3(_position.x, _position.y, _position.z);
            _position.x += (27 / randomNum);
        }
        var _randomNum = UnityEngine.Random.Range(0, (randomNum - 1));
        _boxList[_randomNum].GetComponent<BoxObject>().SetIsAnswer(true);
    }

    /// <summary>
    /// randomly set number of generate objects
    /// </summary>
    /// <returns></returns>
    private int RandomObjectNumber() => UnityEngine.Random.Range(2, 6);


    private void ActiveFalseAllBox()
    {
        for(int count = 0; count < _boxList.Count; count++)
        {
            _boxList[count].gameObject.SetActive(false);
        }
    }
}
