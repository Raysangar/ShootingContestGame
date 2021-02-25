using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    public static AudioManager Instance { get; private set; }

    public AudioLibrary Library { get; private set; }

    public AudioManager(PlayerController player, AudioLibrary library)
    {
        Instance = this;

        this.Library = library;

        sourcesPool = new List<AudioSource>(MaxAudioPool);
        for (int i = 0; i < MaxAudioPool; ++i)
            sourcesPool.Add(player.gameObject.AddComponent<AudioSource>());

        mainMusicSource = player.gameObject.AddComponent<AudioSource>();
        mainMusicSource.clip = library.mainMusic;
        mainMusicSource.loop = true;
        mainMusicSource.Play();
    }

    public void PlayClickAudio()
    {
        PlayAudio(Library.defaultButtonClick);
    }

    public void PlayAudio(AudioClip clip)
    {
        var source = sourcesPool.Find(IsSourceAvailable);
        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
    }

    private bool IsSourceAvailable(AudioSource audioSource) => !audioSource.isPlaying;

    private const int MaxAudioPool = 5;

    private List<AudioSource> sourcesPool;
    private AudioSource mainMusicSource;
}
