using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmArea : InteractiveObject
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _soundChangeSpeed;
    
    [SerializeField] public bool IsPlaying;

    protected override void Awake ()
    {
        base.Awake ();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _audioSource.clip = _sound;
        IsPlaying = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<Thief>())
        {
            TurnOn();
        }
    }

    private void OnTriggerExit2D (Collider2D collider2D)
    {
        if (collider2D.GetComponent<Thief>())
        {
            TurnOff();
        }
    }

    public void TurnOff ()
    {
        StopCoroutine(StartPlaying(_maxVolume));
        StartCoroutine(StopPlaying());
    }

    public void TurnOn ()
    {
        StopCoroutine(StopPlaying());
        StartCoroutine(StartPlaying(_maxVolume));
    }

    private void IncreaseLoudness ()
    {
        _audioSource.volume += _soundChangeSpeed * Time.deltaTime;

        if (_audioSource.volume > _maxVolume)
            _audioSource.volume = _maxVolume;
    }

    private void DecreaseLoudness()
    {
        _audioSource.volume -= _soundChangeSpeed * Time.deltaTime;

        if (_audioSource.volume < 0)
            _audioSource.volume = 0;
    }

    private IEnumerator StartPlaying(float maxLoudness)
    {
        _audioSource.Play();
        IsPlaying = true;

        while (_audioSource.volume < maxLoudness)
        {
            IncreaseLoudness();
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator StopPlaying()
    {
        while (_audioSource.volume > 0)
        {
            DecreaseLoudness();
            yield return new WaitForSeconds(Time.deltaTime);
        }

        _audioSource.Stop();
        IsPlaying = false;
    }
}
