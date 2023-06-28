using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Button _closeBtn;

    private void Start()
    {
        _closeBtn.onClick.AddListener(() => {
            CacheData.Instance.Tutorial = true;
            this.gameObject.SetActive(false);
        });
    }
}
