using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CliffLeeCL;

/*

1. Manage level controls.
2. Access ScoreManagr to utilize scores and hits.

A level should have contain ScoreManager.cs to manage scores.

 */

public class GameManager : MonoBehaviour
{
    public void LoadLevel()
    {
        //Display Result page.

        //Update result.
    }

    public void GameStart()
    {
        //Need to LoadLevel() to get the data a level needed first.
        //Start a game, play Audio and start sending notes.
        EventManager.Instance.OnGameStart();
    }

    public void GameFinish()
    {

    }
}
