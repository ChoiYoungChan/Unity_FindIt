using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonClass<SoundManager>
{
    [System.Serializable]
    public struct SoundInfo
    {
        public string id;
        public AudioClip clip;
        public SoundType type;
        public bool loop;
        [Range(0, 1)] public float volume;
    }

    public enum SoundType { SoundEffect, Music }

    [SerializeField] SoundInfo[] _soundInfos;
    Dictionary<string, SoundInfo> _soundInfoDict = new Dictionary<string, SoundInfo>();
    Dictionary<string, AudioSource> _loopingAudioSourceDict = new Dictionary<string, AudioSource>();

    void Awake()
    {
        for (int i = 0; i < _soundInfos.Length; i++)
        {
            _soundInfoDict.Add(_soundInfos[i].id, _soundInfos[i]);
        }
    }

    /// <summary>
    /// play sound
    /// </summary>
    /// <param name="id"></param>
    public void Play(string id)
    {
        if (!GameManager.Instance.GetSoundOn()) return;

        AudioSource audioSource = GetAudioSource(id);
        SoundInfo soundInfo = _soundInfoDict[id];

        audioSource.clip = soundInfo.clip;
        audioSource.volume = soundInfo.volume;
        audioSource.loop = _soundInfoDict[id].loop;
        if (audioSource.isPlaying) return;
        audioSource.Play();

        if (!_soundInfoDict[id].loop)
            StartCoroutine(FinishPlayback_Coroutine(audioSource));
    }

    /// <summary>
    /// pause sound
    /// </summary>
    public void Pause()
    {
        foreach (var audioSource in _loopingAudioSourceDict)
        {
            audioSource.Value.Pause();
        }
    }

    /// <summary>
    /// get audio source file
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    AudioSource GetAudioSource(string id)
    {
        if (_soundInfoDict[id].loop && _loopingAudioSourceDict.ContainsKey(id))
            return _loopingAudioSourceDict[id];

        Debug.Log("### SoundId : " + id);

        GameObject obj = new GameObject("sound_" + id);
        obj.transform.SetParent(transform);

        AudioSource audioSource = obj.AddComponent<AudioSource>();

        if (_soundInfoDict[id].loop)
            _loopingAudioSourceDict.Add(id, audioSource);

        return audioSource;
    }

    IEnumerator FinishPlayback_Coroutine(AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Destroy(audioSource.gameObject);
    }
}
