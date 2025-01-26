using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GlobalSettings settings;

    private AudioSource _audio;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _audio.volume = settings.musicVolume;
    }
}
