using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    [SerializeField] private float _volumeChangeRate;

    private AudioSource _sound;

    private Coroutine _increaseVolume;
    private Coroutine _decreaseVolume;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Theif theif))
        {
            StopChangeVolume();

            _sound.Play();

            _increaseVolume = StartCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Theif theif))
        {
            StopChangeVolume();

            _decreaseVolume = StartCoroutine(DecreaseVolume(() => _sound.Stop()));
        }
    }

    private void StopChangeVolume()
    {
        if(_increaseVolume != null)
            StopCoroutine(_increaseVolume);

        if(_decreaseVolume != null)
            StopCoroutine(_decreaseVolume);
    }

    private IEnumerator IncreaseVolume()
    {
        float currentVolume = _sound.volume;

        while(currentVolume < MaxVolume)
        {
            currentVolume += Time.deltaTime * _volumeChangeRate;
            _sound.volume = Mathf.Lerp(MinVolume, MaxVolume, currentVolume);

            yield return null;
        }
    }

    private IEnumerator DecreaseVolume(Action callback)
    {
        float currentVolume = _sound.volume;

        while (currentVolume > 0)
        {
            currentVolume -= Time.deltaTime * _volumeChangeRate;
            _sound.volume = Mathf.Lerp(MinVolume, MaxVolume, currentVolume);

            yield return null;
        }

        callback?.Invoke();
    }
}
