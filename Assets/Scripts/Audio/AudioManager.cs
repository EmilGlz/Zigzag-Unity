using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    #endregion
    public Sound[] sounds;
    public void Play(int soundIndex)
    {
        Sound s = sounds[soundIndex];
        if (s == null || !ProjectController.Instance.SoundOn)
            return;
        s.source.Play();
    }
    public void Stop(int soundIndex)
    {
        Sound s = sounds[soundIndex];
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }
    public void SetVolume(int soundIndex, float soundVolume)
    {
        Sound s = sounds[soundIndex];
        if (s == null)
        {
            return;
        }
        s.source.volume = soundVolume;
    }
}