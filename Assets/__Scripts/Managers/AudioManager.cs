using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    SFX,
    Music,
}

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

    public void PlayOnTarget(GameObject target, Sound sound)
    {
        var sourceObj = target.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);
        Destroy(sourceObj, sound.Clip.length + 0.4f);
    }

    public void PlayAtPosition(Vector3 position, Sound sound)
    {
        GameObject gameObject = new GameObject(sound.name);
        var soundObj = Instantiate(gameObject, position, Quaternion.identity);
        var sourceObj = soundObj.AddComponent<AudioSource>();
        Play(sourceObj, sound, SoundType.SFX);

        Destroy(soundObj, sound.Clip.length + 0.4f);
    }

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

    public void PlayGlobal(Sound sound, SoundType type = SoundType.SFX)
    {       
        if(type == SoundType.Music)
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

    public void SetVolume(float value)
    {
        value = Mathf.Clamp01(value) * 30 - 20; // -20db -- 20db range

        if (value <= -19)
            value = float.MinValue;

        masterMixer.audioMixer.SetFloat("MasterVolume", value);
    }
}