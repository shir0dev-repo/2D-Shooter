using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] AudioClip _soundtrack;
    [Space]
    [SerializeField] private float _sfxVolume = 0.4f;
    [SerializeField] private float _musicVolume = 0.2f;
    [SerializeField] private float _masterVolume = 0.2f;

    private void Awake()
    {
        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.volume = _sfxVolume;
        _sfxSource.clip = null;
        _sfxSource.playOnAwake = false;
        _sfxSource.loop = false;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.volume = _musicVolume;
        _musicSource.clip = _soundtrack;
        _musicSource.playOnAwake = false;
        _musicSource.loop = true;
    }

    private void Start()
    {
        PlayMusic(_soundtrack);
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip, _sfxVolume);
    }
    private void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    // referenced by slider
    public void SetMasterVolume(float value)
    {
        _masterVolume = value;
        _sfxVolume *= _masterVolume;
        _musicVolume *= _masterVolume;
    }

    // referenced by slider
    public void SetSfxVolume(float value)
    {
        _sfxVolume = value * _masterVolume;
        _sfxSource.volume = _sfxVolume;
    }

    // referenced by slider
    public void SetMusicVolume(float value)
    {
        _musicVolume = value * _masterVolume;
        _musicSource.volume = _musicVolume;
    }
}
