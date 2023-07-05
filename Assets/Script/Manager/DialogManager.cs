using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : SingletonClass<DialogManager>
{
    [SerializeField] GameObject[] _dialogList;

    public enum DialogKey {
        DialogKey_Setting = 0,
        DialogKey_Tutorial = 1,
        DialogKey_Max = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// open dialog
    /// </summary>
    /// <param name="_key">dialog enum key</param>
    public void OpenDialog(DialogKey _key)
    {
        _dialogList[(int)_key].gameObject.SetActive(true);
    }
}
