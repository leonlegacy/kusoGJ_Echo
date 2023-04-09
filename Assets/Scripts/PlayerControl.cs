using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;
using Rhythm;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    Rhythm.MusicDatas musicDatas;
    [SerializeField]
    Animator boomRed;

    MusicData currentMusicData;
    int noteAmnt = 1, noteIndex = 0;
    double musicStartDspTime = 0, hitDspTime = 0;
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
        //Debug.Log(musicTime);
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.J))
        {
            CheckNoteHit(BeatType.Red);
            AudioManager.Instance.PlaySound(AudioManager.AudioName.Hit_red);

        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.K))
        {
            CheckNoteHit(BeatType.Blue);
            AudioManager.Instance.PlaySound(AudioManager.AudioName.Hit_blue);
        }
            
    }

    void LoadMusicDatas()
    {
        noteIndex = 0;
    }

    void MusicTimerBegin(Song song)
    {
        musicStartDspTime = AudioSettings.dspTime;
        currentMusicData = musicDatas.musics.Find(x => x.musicId == song.songId); 
        noteAmnt = currentMusicData.beatMap.Count;
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
        if(noteIndex < noteAmnt && currentMusicData.beatMap[noteIndex].beatType == beatType)
        {
            hitDspTime = AudioSettings.dspTime - musicStartDspTime;
            var noteTime = currentMusicData.beatMap[noteIndex].createTime;
            var diffTime = Mathf.Abs((float)hitDspTime - noteTime);
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
