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

    float musicTime = 0;

    private void OnEnable()
    {
        EventManager.Instance.onNewGameLoad += LoadMusicDatas;
        EventManager.Instance.onMusicPlay += MusicTimerBegin;
    }

    private void OnDisable()
    {
        EventManager.Instance.onNewGameLoad -= LoadMusicDatas;
    }

    // Update is called once per frame
    void Update()
    {
        musicTime += Time.deltaTime;
        Debug.Log(musicTime);
    }

    void LoadMusicDatas()
    {
        noteAmnt = musicDatas.musics.Count - 1;
        noteIndex = 0;
    }

    void MusicTimerBegin(int _)
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
         */
    }
}
