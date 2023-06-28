using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : SingletonClass<LayerManager>
{
    [SerializeField] GameObject[] _layerList;

    public enum LayerKey {
        LayerKey_Top = 0,
        LayerKey_Play = 1,
        LayerKey_Result = 2,
        LayerKey_Max = 3
    }

    public void MoveLayer(LayerKey _key)
    {
        for(int count = 0; count < _layerList.Length; count++) {
            if((count == (int)_key)) {
                _layerList[count].gameObject.SetActive(true);
            } else {
                _layerList[count].gameObject.SetActive(false);
            }
        }
    }
}
