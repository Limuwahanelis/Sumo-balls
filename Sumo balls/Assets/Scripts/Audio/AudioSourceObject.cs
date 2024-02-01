using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioSourceObject : MonoBehaviour
{
    public AudioSource AudioSource => _audioSource;
    [SerializeField] AudioSource _audioSource;
    private IObjectPool<AudioSourceObject> _pool;
    public void SetPool(IObjectPool<AudioSourceObject> pool) => _pool = pool;

    public void ReturnToPool() => _pool.Release(this);
    public void ReturnToPool(float delay) => StartCoroutine(ReturnToPoolCor(delay));
    IEnumerator ReturnToPoolCor(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }
}
