using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    Rhythm.MusicDatas musicDatas;

    [SerializeField]
    AudioSource musicSource;

    [SerializeField]
    Animator boomRed;

    Song currentSong;
    int noteAmnt = 1, noteIndex = 0;

    float musicTime = 0, hitTime = 0;

    bool hasMiss = false, hasHit = false;

    private void OnEnable()
    {
        EventManager.Instance.onNewGameLoad += LoadMusicDatas;
        EventManager.Instance.onMusicPlay += MusicTimerBegin;
        EventManager.Instance.onBeatRecycle += NoteIndexCounter;
    }

    private void OnDisable()
    {
        EventManager.Instance.onNewGameLoad -= LoadMusicDatas;
        EventManager.Instance.onMusicPlay -= MusicTimerBegin;
        EventManager.Instance.onBeatRecycle -= NoteIndexCounter;
    }

    // Update is called once per frame
    void Update()
    {
        musicTime += Time.deltaTime;
        //Debug.Log(musicTime);
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.J))
            CheckNoteHit(BeatType.Red);
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.K))
            CheckNoteHit(BeatType.Blue);
    }

    void LoadMusicDatas()
    {
        noteIndex = 0;
    }

    void MusicTimerBegin(Song song)
    {
        musicTime = 0;
        noteAmnt = musicDatas.musics[0].beatMap.Count;
        currentSong = song;
        ScoreManager.Instance.TotalNotes = noteAmnt;
    }

    void CheckNoteHit(BeatType beatType)
    {
        /*
         *  Note launchs at 3.6s [CreateTime]
         *  Note flies by 2s
         * 
         *  Note lands at 5.6s
         *  
         *  Player hits at 5.3s
         *  
         *  : [TD] Time difference: abs(5.6s - 5.3s) = 0.3s
         * 
         *  TD < 0.2s       = Perfect
         *  0.2s < TD < 0.5 = Good
         *  TD < 0.5s       = Bad
         *  
         *  TH = 0.2f
         *  
         *  //How to check miss?
         *  
         *  //Event to hitable first
         */
        boomRed.StopPlayback();
        boomRed.Play("Boom");
        var currentMusicData = musicDatas.musics.Find(x => x.musicId == currentSong.songId);
        if(noteIndex < noteAmnt && currentMusicData.beatMap[noteIndex].beatType == beatType)
        {
            hitTime = musicTime;
            float noteTime = currentMusicData.beatMap[noteIndex].createTime;
            float diffTime = Mathf.Abs(hitTime - noteTime);
            if (diffTime < 0.15f)
            {
                ScoreManager.Instance.HitToScore(HitType.Perfect);
            }
            else if (0.15f < diffTime && diffTime <= 0.3f)
            {
                ScoreManager.Instance.HitToScore(HitType.Good);

            }
            else if (0.3f < diffTime)
            {
                ScoreManager.Instance.HitToScore(HitType.Bad);
            }
            //noteIndex++;
            hasHit = true;
        }

        //noteIndex++;
    }

    void NoteIndexCounter()
    {
        noteIndex++;
    }
}
