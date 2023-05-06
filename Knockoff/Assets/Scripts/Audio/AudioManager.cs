using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioMixer mainMixer;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;

    public AudioClip mainMenuMusic;
    public AudioClip[] gameplayMusic;

    private AudioSource MusicAudioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (GetComponent<AudioSource>() != null)
            MusicAudioSource = GetComponent<AudioSource>();
        else
            Debug.LogError("No audio source component attached to AudioManager");
    }

    public void ChangeToGameplayMusic()
    {
        MusicAudioSource.Stop();
        MusicAudioSource.clip = gameplayMusic[0];
        MusicAudioSource.Play();

    }


    /*
    public void PlayStopSound(bool play)
    {
        float volume = play ? 0f : -80f;
        sfxGroup.audioMixer.SetFloat("sfxVol", volume);
        PlayerSoundPreference.SoundOnOffPreference(play ? 0 : 1);
    }

    public void PlayStopMusic(bool play)
    {
        PlayerSoundPreference.MusicOnOffPreference(play ? 0 : 1);

        if (!play)
            foreach (Sound s in music)
                s.audioSource.Stop();
        else
            Play("GlobalMusic");
    }

    public void Play(string name)
    {
        if (PlayerSoundPreference.GetMusicOnOffPreference() != 1)
        {
            Sound s = Array.Find(music, sound => sound.soundName == name);

            foreach (Sound m in music)
                if (s != m)
                    m.audioSource.Stop();

            if (s != null && !s.audioSource.isPlaying)
                s.audioSource.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(music, sound => sound.soundName == name);

        if (s != null)
            s.audioSource.Stop();
    }*/
}
