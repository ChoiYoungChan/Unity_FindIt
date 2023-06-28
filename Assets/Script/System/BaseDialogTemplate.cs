using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialogTemplate : SingletonClass<BaseDialogTemplate>
{
    public virtual void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public virtual void Initialize()
    {

    }

    public virtual void UpdateData()
    {

    }
}
