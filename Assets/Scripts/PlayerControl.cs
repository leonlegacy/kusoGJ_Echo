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

    int noteAmnt = 1, noteIndex = 0;

    float musicTime = 0, hitTime = 0;

    bool hasMiss = false;

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
        Debug.Log(musicTime);
        if (Input.GetKeyDown(KeyCode.J))
            CheckNoteHit();
    }

    void LoadMusicDatas()
    {
        noteAmnt = musicDatas.musics.Count;
        noteIndex = 0;
        ScoreManager.Instance.TotalNotes = noteAmnt;
    }

    void MusicTimerBegin(Song _)
    {
        musicTime = 0;
    }

    void CheckNoteHit()
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
         */

        hitTime = musicTime;
        float noteTime = musicDatas.musics[0].beatMap[noteIndex].createTime;
        float diffTime = Mathf.Abs(hitTime - noteTime);
        if (diffTime < 0.2f)
            ScoreManager.Instance.HitToScore(HitType.Perfect);
        else if (0.2f <= diffTime && diffTime <= 0.5f)
            ScoreManager.Instance.HitToScore(HitType.Good);
        else
            ScoreManager.Instance.HitToScore(HitType.Bad);

        //noteIndex++;
    }

    void NoteIndexCounter()
    {
        noteIndex++;
    }

    IEnumerator MissCheck()
    {
        yield return new WaitForSeconds(0.5f);
        if (hasMiss)
            noteIndex++;
    }
}
