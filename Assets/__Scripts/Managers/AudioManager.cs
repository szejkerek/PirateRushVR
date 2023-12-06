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

    public void Play(Sound sound, AudioSource source, SoundType type)
    {
        if (sound == null)
        {
            Debug.LogWarning("Sound is null");
            return;
        }

        if (source == null)
        {
            Debug.LogWarning("Audio source is null");
            return;
        }

        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.pitch = sound.Pitch;
        source.loop = sound.Loop;

        SetMixer(source, type);

        source.Play();
    }

    public void PlayGlobal(Sound sound, SoundType type = SoundType.SFX)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        Play(sound, source, type);

        Destroy(source, source.clip.length + 0.25f);
    }

    public void PlayMusic(Sound sound)
    {
        Play(sound, musicSource, SoundType.Music);
    }

    public void StopMusic()
    {
        musicSource.Stop();
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
        value = Mathf.Clamp01(value) * 40 - 20; // -20db -- 20db range

        if (value <= -18)
            value = float.MinValue;

        masterMixer.audioMixer.SetFloat("MasterVolume", value);
    }
}