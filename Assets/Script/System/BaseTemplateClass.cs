using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTemplateClass : SingletonClass<BaseTemplateClass>
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
}
