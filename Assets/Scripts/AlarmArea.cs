using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmArea : InteractiveObject
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _soundChangeSpeed;
    
    private AudioSource _audioSource;
    private bool _isPlaying;

    public bool IsPlaying => _isPlaying;

    public void TurnOff()
    {
        StopCoroutine(StartPlaying(_maxVolume));
        StartCoroutine(StopPlaying());
    }

    public void TurnOn()
    {
        StopCoroutine(StopPlaying());
        StartCoroutine(StartPlaying(_maxVolume));
    }

    protected override void Awake ()
    {
        base.Awake ();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
        _audioSource.clip = _sound;
        _isPlaying = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.GetComponent<Thief>())
        {
            TurnOn();
        }
    }

    private void OnValidate()
    {
        if (_maxVolume < 0)
            _maxVolume = Random.Range(0f, 1f);

        if (_soundChangeSpeed < 0)
            _soundChangeSpeed = Random.Range(0f, 1f);
    }

    private void OnTriggerExit2D (Collider2D collider2D)
    {
        if (collider2D.GetComponent<Thief>())
        {
            TurnOff();
        }
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
        _isPlaying = true;

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
        _isPlaying = false;
    }
}
