using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DelayControlClass : SingletonClass<DelayControlClass>
{
    public void CallAfter(float time, Action action)
    {
        this.StartCoroutine(DoCallWaitForSeconds(time, action));
    }

    public void WaitOneFrame(Action action)
    {
        this.StartCoroutine(DoCallWaitForOneFrame(action));
    }

    public async void DelayMethod(float time)
    {
        int delaytime = (int)time * 1000;
        await Task.Delay(delaytime);
    }

    private IEnumerator DoCallWaitForSeconds(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    private IEnumerator DoCallWaitForOneFrame(Action action)
    {
        yield return 0;
        action();
    }

}
