using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    private bool _isTouched;
    [SerializeField] Animator _animator;
    [SerializeField] ParticleSystem _particle;
    [SerializeField] LootBox _lootBox;
    [SerializeField] GameObject _coin;
    [SerializeField] bool _isAnswer;


    public void Initialize()
    {
        _lootBox.BounceBox(false);
        _particle.gameObject.SetActive(false);
        _particle.Stop();
        _isTouched = false;
        _isAnswer = false;
        _lootBox.BounceBox(true);
        _coin.SetActive(false);
    }

    IEnumerator OpenAnime()
    {
        _lootBox.Open();

        _particle.gameObject.SetActive(true);
        _particle.Play();
        yield return new WaitForSeconds(1.0f);
    }

    public void SetIsAnswer(bool _answer)
    {
        _isAnswer = _answer;
        _coin.SetActive(_answer);
    }

    public bool GetIsAnswer()
    {
        return _isAnswer;
    }

    public void OnAction(Action _action)
    {
        if (!_isTouched) {
            StartCoroutine(OpenAnime());
            GameManager.Instance.SetIsClear(_isAnswer);
            DelayControlClass.Instance.CallAfter(2.0f, () => {
                LayerManager.Instance.MoveLayer(LayerManager.LayerKey.LayerKey_Result);
                _action();
                _lootBox.Close();
            });
        }
    }
}
