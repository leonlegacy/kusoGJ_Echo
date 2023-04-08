using System.Collections.Generic;
using UnityEngine;

public class Song{
    public string songName;
    public float duration;
    public int beatsPerMinute;

    public Song()
    {
        songName = "NoName";
        duration = 0.0f;
        beatsPerMinute = 0;
    }

    // Copy constructor
    public Song(Song songToBeCopied)
    {
        songName = songToBeCopied.songName;
        duration = songToBeCopied.duration;
        beatsPerMinute = songToBeCopied.beatsPerMinute;
    }

    public Song(string songName, float duration, int beatsPerMinute)
    {
        this.songName = songName;
        this.duration = duration;
        this.beatsPerMinute = beatsPerMinute;
    }
}
