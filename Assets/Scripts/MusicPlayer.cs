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
    
    public double lastPlayedDspTime;
    public string currentSongName;
    public Dictionary<string, Song> songNameToSongDict;

    [SerializeField] float songDelayTime;
    [SerializeField] bool isSongPlayed;

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
        if (Input.GetKeyDown(KeyCode.E))
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
        isSongPlayed = true;
        StartCoroutine(DelayMusicPlayEvent(songName));
    }

    IEnumerator DelayMusicPlayEvent(string songName)
    {
        yield return new WaitForSeconds(songDelayTime);
        EventManager.Instance.OnMusicPlay(songNameToSongDict[songName]);
        lastPlayedDspTime = AudioSettings.dspTime;
    }

    public double StopSong()
    {
        AudioManager.Instance.StopMusic();
        isSongPlayed = false;

        return AudioSettings.dspTime;
    }
}
