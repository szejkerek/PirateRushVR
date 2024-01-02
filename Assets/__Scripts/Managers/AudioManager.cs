using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Enum defining different types of sounds.
/// </summary>
public enum SoundType
{
    SFX,
    Music,
}

/// <summary>
/// Manages the audio for the game.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioMixerGroup masterMixer;
    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] AudioMixerGroup musicMixer;

    public SFXLib SFXLib => _SFXLib;
    [SerializeField] SFXLib _SFXLib;

    public MusicLib MusicLib => musicLib;
    [SerializeField] MusicLib musicLib;

    [SerializeField] AudioSource musicSource;

    protected override void Awake()
    {
        base.Awake();
        SetMixer(musicSource, SoundType.Music);
    }

    private void Start()
    {
        float volume = GlobalSettingManager.Instance.GetVolume();
        SetVolume(volume);
    }

    /// <summary>
    /// Plays a sound on a target GameObject.
    /// </summary>
    public void PlayOnTarget(GameObject target, Sound sound)
    {
        var sourceObj = target.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);
        Destroy(sourceObj, sound.Clip.length + 0.4f);
    }

    /// <summary>
    /// Plays a sound at a specific position in the scene.
    /// </summary>
    public void PlayAtPosition(Vector3 position, Sound sound)
    {
        GameObject gameObject = new GameObject(sound.name);
        var soundObj = Instantiate(gameObject, position, Quaternion.identity);
        var sourceObj = soundObj.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);
        Destroy(soundObj, sound.Clip.length + 0.4f);
    }

    /// <summary>
    /// Plays a sound using the provided AudioSource and Sound parameters.
    /// </summary>
    public void Play(AudioSource source, Sound sound, SoundType type)
    {
        if (sound == null)
        {
            Debug.LogWarning($"Sound of {source.gameObject.name} is null");
            return;
        }

        source.clip = sound.Clip;
        source.volume = sound.Volume;

        if (sound.PitchVariation > 0)
        {
            source.pitch = 1 + Random.Range(-sound.PitchVariation, sound.PitchVariation);
        }
        else
        {
            source.pitch = 1;
        }

        source.loop = sound.Loop;

        source.spatialBlend = sound.Settings3D.SpatialBlend ? 1f : 0f;
        source.minDistance = sound.Settings3D.MinDistance;
        source.maxDistance = sound.Settings3D.MaxDistance;

        SetMixer(source, type);

        source.Play();
    }

    /// <summary>
    /// Plays a sound globally.
    /// </summary>
    public void PlayGlobal(Sound sound, SoundType type = SoundType.SFX)
    {
        if (type == SoundType.Music)
        {
            musicSource.Stop();
            Play(musicSource, sound, SoundType.Music);
            StartCoroutine(FadeInMusic(sound, 1f));
        }
        else
        {
            PlayOnTarget(gameObject, sound);
        }
    }

    /// <summary>
    /// Fades in the music over a specified duration.
    /// </summary>
    private IEnumerator FadeInMusic(Sound sound, float duration)
    {
        float startVolume = 0.0f;
        musicSource.volume = startVolume;

        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, sound.Volume, currentTime / duration);
            yield return null;
        }

        musicSource.volume = sound.Volume;
    }

    /// <summary>
    /// Sets the audio mixer for the given AudioSource based on the SoundType.
    /// </summary>
    private void SetMixer(AudioSource source, SoundType type)
    {
        switch (type)
        {
            case SoundType.SFX:
                source.outputAudioMixerGroup = sfxMixer;
                break;
            case SoundType.Music:
                source.outputAudioMixerGroup = musicMixer;
                break;
        }
    }

    /// <summary>
    /// Sets the volume level for the audio mixer.
    /// </summary>
    public void SetVolume(float value)
    {
        value = Mathf.Clamp01(value) * 30 - 20; // -20db -- 20db range

        if (value <= -19)
            value = float.MinValue;

        masterMixer.audioMixer.SetFloat("MasterVolume", value);
    }
}
