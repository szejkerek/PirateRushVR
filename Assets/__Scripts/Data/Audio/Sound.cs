using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound", order = 1)]
public class Sound : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { private set; get; }
    public float Volume => volume;
    [SerializeField, Range(0, 1)] float volume = 1;
    public float Pitch => pitch;
    [SerializeField, Range(-3, 3)] float pitch = 1;    
    public bool Loop => loop;
    [SerializeField] bool loop = false;    
}