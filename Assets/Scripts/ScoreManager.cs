using UnityEngine;
using CliffLeeCL;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum HitType
{
    Perfect,
    Good,
    Bad
}

public class ScoreManager : SingletonMono<ScoreManager>
{
    public float CurrentScore { get; set; }
    public int PerfectHits { get; set; }    // 1000/41 = 100% of a note
    public int GoodHits { get; set; }
    public int BadHits { get; set; }

    public int TotalNotes = 5;  //How to update this number? => MusicDatas

    public float TotalScore = 10000f;

    public float PerfectScoreRatio = 1f;

    public float GoodScoreRatio = .5f;

    public float BadScoreRatio = 0f;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        CurrentScore = TotalScore;
        PerfectHits = 0;
        GoodHits = 0;
        BadHits = 0;
    }

    public void HitToScore(HitType type)
    {
        //Called when a note is hit.
        float scored, scoreRatio;

        switch(type)
        {
            case HitType.Perfect:
                scoreRatio = PerfectScoreRatio;
                PerfectHits += 1;
                break;

            case HitType.Good:
                scoreRatio = GoodScoreRatio;
                GoodHits += 1;
                break;

            case HitType.Bad:
                scoreRatio = BadScoreRatio;
                BadHits += 1;
                break;

            default:
                scoreRatio = BadScoreRatio;
                BadHits += 1;
                break;
        }

        scored = (TotalScore / TotalNotes) * scoreRatio;
        CurrentScore -= scored;
        EventManager.Instance.OnPlayerScored();
    }
}

//Debug UI
#if UNITY_EDITOR
[CustomEditor(typeof(ScoreManager))]
class ScoreManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScoreManager debugger = (ScoreManager)target;

        if(GUILayout.Button("Hit Perfect."))
        {
            debugger.HitToScore(HitType.Perfect);
        }

        if(GUILayout.Button("Hit Good."))
        {
            debugger.HitToScore(HitType.Good);
        }

        if(GUILayout.Button("Hit Bad."))
        {
            debugger.HitToScore(HitType.Bad);
        }
    }
}
#endif