using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText, perfectText, goodText, badText;

    [SerializeField]
    ScoreManager scoreManager;

    private void Start()
    {
        DisplayResult();
    }

    public void DisplayResult()
    {
        scoreText.text = scoreManager.TotalScore.ToString();
        perfectText.text = scoreManager.TotalNotes.ToString();
        goodText.text = scoreManager.GoodHits.ToString();
        badText.text = scoreManager.BadHits.ToString();
    }
}
