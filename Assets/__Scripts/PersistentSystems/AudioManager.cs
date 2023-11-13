using UnityEngine;

public class AudioManager : Singleton<AudioManager> 
{
    public SFXLib SFXLib => _SFXLib;
    [SerializeField] SFXLib _SFXLib;

    public MusicLib MusicLib => musicLib;
    [SerializeField] MusicLib musicLib;

    [SerializeField] AudioSource musicSource;

    public void Play(Sound sound, AudioSource source)
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

        source.Play();
    }

    public void PlayGlobal(Sound sound)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        Play(sound, source);

        Destroy(source, source.clip.length + 0.25f);
    }

    public void PlayMusic(Sound sound)
    {
        Play(sound, musicSource);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}