using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText, perfectText, goodText, badText, realtimeScoreText;

    private void OnEnable()
    {
        realtimeScoreText.text = ScoreManager.Instance.TotalScore.ToString("0");
        CliffLeeCL.EventManager.Instance.onNewGameLoad += DisplayResult;
        CliffLeeCL.EventManager.Instance.onPlayerScored += UpdateScore;
    }

    private void OnDisable()
    {
        CliffLeeCL.EventManager.Instance.onNewGameLoad -= DisplayResult;
        CliffLeeCL.EventManager.Instance.onPlayerScored -= UpdateScore;
    }

    public void DisplayResult()
    {
        if (scoreText) scoreText.text = ScoreManager.Instance.TotalScore.ToString();
        if (perfectText) perfectText.text = ScoreManager.Instance.TotalNotes.ToString();
        if (goodText) goodText.text = ScoreManager.Instance.GoodHits.ToString();
        if (badText) badText.text = ScoreManager.Instance.BadHits.ToString();
    }

    void UpdateScore()
    {
        realtimeScoreText.text = ScoreManager.Instance.CurrentScore.ToString("0");
    }
}
