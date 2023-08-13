using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    [SerializeField] Animator Animator;
    [SerializeField] ParticleSystem Particle;
    [SerializeField] LootBox LootBox;
    [SerializeField] GameObject Coin;

    private bool b_isTouched;
    private bool b_isAnswer;

    public void Initialize()
    {
        LootBox.BounceBox(false);
        Particle.gameObject.SetActive(false);
        Particle.Stop();
        b_isTouched = false;
        b_isAnswer = false;
        LootBox.BounceBox(true);
        Coin.SetActive(false);
    }

    /// <summary>
    /// play animation and particle
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenAnime()
    {
        LootBox.Open();

        Particle.gameObject.SetActive(true);
        Particle.Play();
        yield return new WaitForSeconds(1.0f);
    }

    /// <summary>
    /// set amswer amd setactove cpom object
    /// </summary>
    /// <param name="_answer"></param>
    public void SetIsAnswer(bool _answer)
    {
        b_isAnswer = _answer;
        Coin.SetActive(_answer);
    }

    /// <summary>
    /// get bpp; value isAnswer
    /// </summary>
    /// <returns></returns>
    public bool GetIsAnswer()
    {
        return b_isAnswer;
    }

    /// <summary>
    /// set action when this box object is selected
    /// </summary>
    /// <param name="_action"></param>
    public void SetAction(Action _action)
    {
        if (!b_isTouched) {
            StartCoroutine(OpenAnime());
            GameManager.Instance.SetIsClear(b_isAnswer);
            DelayControlClass.Instance.CallAfter(2.0f, () => {
                LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
                _action();
                LootBox.Close();
            });
        }
    }
}
