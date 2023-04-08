using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class MusicPlayer : SerializedMonoBehaviour {
    /// <summary>
    /// The variable is used to access this class.
    /// </summary>
    public static MusicPlayer Instance;
    
    public Dictionary<AudioManager.AudioName, Song> audioNameToSongDict;
    
    [SerializeField] float songDelayTime;
    [SerializeField] bool isSongPlayed;
    double lastPlayedDspTime;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        EventManager.Instance.onGameStart += OnGameStart;
    }

    void OnDisable()
    {
        EventManager.Instance.onGameStart -= OnGameStart;
    }

    void OnGameStart()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlayMusic(AudioManager.AudioName.GameSong1, songDelayTime);
            lastPlayedDspTime = AudioSettings.dspTime + songDelayTime;
            isSongPlayed = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioManager.Instance.PlayMusicReversed(AudioManager.AudioName.GameSong1, songDelayTime);
            lastPlayedDspTime = AudioSettings.dspTime + songDelayTime;
            isSongPlayed = true;
        }

        if (isSongPlayed && !AudioManager.Instance.IsMusicPlaying())
        {
            isSongPlayed = false;
            EventManager.Instance.OnGameOver();
        }
    }

    public double StopSong()
    {
        AudioManager.Instance.StopMusic();
        isSongPlayed = false;

        return AudioSettings.dspTime;
    }
}
