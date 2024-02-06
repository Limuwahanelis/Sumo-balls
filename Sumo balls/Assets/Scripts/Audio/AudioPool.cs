using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioPool : MonoBehaviour
{
    [SerializeField] AudioSourceObject _audioSourceGameobjectPrefab;
    private ObjectPool<AudioSourceObject> _audioSourceObjectPool;
    private List<AudioSourceObject> _audioSourceObjects=new List<AudioSourceObject>();
    // Start is called before the first frame update
    void Awake()
    {
        _audioSourceObjectPool = new ObjectPool<AudioSourceObject>(CreateAudioSourceObject, OnTakeAudioSourceObjectFromPool, OnReturnAudioSourceObjectToPool);
    }

    public AudioSourceObject GetAudioSourceObject()
    {
        return _audioSourceObjectPool.Get();
    }
    AudioSourceObject CreateAudioSourceObject()
    {
        AudioSourceObject audioSourceObject = Instantiate(_audioSourceGameobjectPrefab);
        audioSourceObject.SetPool(_audioSourceObjectPool);
        return audioSourceObject;

    }
    public void OnTakeAudioSourceObjectFromPool(AudioSourceObject source)
    {
        if(!_audioSourceObjects.Contains(source)) _audioSourceObjects.Add(source);
        source.gameObject.SetActive(true);

    }
    public void OnReturnAudioSourceObjectToPool(AudioSourceObject source)
    {
        source.AudioSource.pitch = 1;
        source.gameObject.SetActive(false);
    }

    public void ReturnAllAudioSourcesToPool()
    {
        foreach(AudioSourceObject audioSourceObject in _audioSourceObjects)
        {
            if(audioSourceObject.gameObject.activeSelf) audioSourceObject.ReturnToPool();
        }
    }
}
