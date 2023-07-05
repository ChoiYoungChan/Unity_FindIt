using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopLayer : BaseLayerTemplate
{
    [SerializeField] Button _startBtn;

    public override void Awake()
    {
        _startBtn.onClick.AddListener(OnClickStartButton);
    }

    public override void OnEnable()
    {
        SoundManager.Instance.Play("bgm");
    }

    private void OnClickStartButton()
    {
        LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Play);
    }
}
