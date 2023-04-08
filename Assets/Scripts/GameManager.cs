using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

1. Manage level controls.
2. Access ScoreManagr to utilize scores and hits.

A level should have contain ScoreManager.cs to manage scores.

 */

public class GameManager : MonoBehaviour
{
    ScoreManager loadedScoreManager;

    public void LoadLevel()
    {

        loadedScoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
    }
}
