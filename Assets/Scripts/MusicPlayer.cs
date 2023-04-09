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
    
    public string currentSongName;
    public Dictionary<string, Song> songNameToSongDict;

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
        PlaySong(currentSongName, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlaySong(currentSongName, false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlaySong(currentSongName, true);
        }

        if (isSongPlayed && !AudioManager.Instance.IsMusicPlaying())
        {
            isSongPlayed = false;
            EventManager.Instance.OnGameOver();
        }
    }

    public void PlaySong(string songName, bool isReversed)
    {
        if (!songNameToSongDict.ContainsKey(songName)) 
            return;

        if (isReversed)
            AudioManager.Instance.PlayMusicReversed(songNameToSongDict[songName].audioName, songDelayTime);
        else
            AudioManager.Instance.PlayMusic(songNameToSongDict[songName].audioName, songDelayTime);
        lastPlayedDspTime = AudioSettings.dspTime + songDelayTime;
        isSongPlayed = true;
        EventManager.Instance.OnMusicPlay(songNameToSongDict[songName]);
    }

    public double StopSong()
    {
        AudioManager.Instance.StopMusic();
        isSongPlayed = false;

        return AudioSettings.dspTime;
    }
}
