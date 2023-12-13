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


    public void Play(GameObject target, Sound sound, SoundType type)
    {
        AudioSource source = target.AddComponent<AudioSource>();
        if (sound == null)
        {
            Debug.LogWarning($"Sound of {target.name} is null");
            return;
        }

        source.clip = sound.Clip;
        source.volume = sound.Volume;

        if (sound.ShouldRandomizePitch)
        {
            source.pitch = sound.InitialPitch + Random.Range(-sound.PitchVariation, sound.PitchVariation);
        }
        else
        {
            source.pitch = sound.InitialPitch;
        }

        source.loop = sound.Loop;

        source.spatialBlend = sound.Settings3D.SpatialBlend ? 1f : 0f;
        source.minDistance = sound.Settings3D.MinDistance;
        source.maxDistance = sound.Settings3D.MaxDistance;

        SetMixer(source, type);

        Destroy(source, sound.Clip.length + 0.1f);

        source.Play();
    }

    public void PlayGlobal(Sound sound, SoundType type = SoundType.SFX)
    {       
        Play(gameObject, sound, type);
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

        if (value <= -19)
            value = float.MinValue;

        masterMixer.audioMixer.SetFloat("MasterVolume", value);
    }
}