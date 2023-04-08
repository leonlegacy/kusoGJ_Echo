using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText, perfectText, goodText, badText;

    [SerializeField]
    ScoreManager scoreManager;

    public void Awake()
    {
        scoreManager.NewGame();
    }

    public void DisplayResult()
    {
        scoreText.text = scoreManager.TotalScore.ToString();
        perfectText.text = scoreManager.PerfectHits.ToString();
        goodText.text = scoreManager.GoodHits.ToString();
        badText.text = scoreManager.BadHits.ToString();
    }

    public void GameStart()
    {
        //Start the main game.
    }
}
