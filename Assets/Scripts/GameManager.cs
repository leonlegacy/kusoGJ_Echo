using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CliffLeeCL;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*

1. Manage level controls.
2. Access ScoreManagr to utilize scores and hits.

A level should have contain ScoreManager.cs to manage scores.

 */

public class GameManager : SingletonMono<GameManager>
{

    private void OnEnable()
    {
        EventManager.Instance.onNewGameLoad += LoadLevel;
        EventManager.Instance.onGameOver += (() => StartCoroutine(GameOverIE()));
        EventManager.Instance.onNewGame += (() => StartCoroutine(NewGameIE()));
    }

    private void OnDisable()
    {
        EventManager.Instance.onNewGameLoad -= LoadLevel;
        EventManager.Instance.onGameOver -= (() => StartCoroutine(GameOverIE()));
        EventManager.Instance.onNewGame -= (() => StartCoroutine(NewGameIE()));
    }

    private void Awake()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

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
    
    IEnumerator GameOverIE()
    {
        yield return null;
        SceneManager.LoadScene("Trans", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.1f);
        SceneManager.UnloadScene("Game");
        SceneManager.LoadScene("EndScene", LoadSceneMode.Additive);
    }

    IEnumerator NewGameIE()
    {
        yield return null;
        SceneManager.UnloadScene("Trans");
        SceneManager.LoadScene("Trans", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.1f);
        SceneManager.UnloadScene("EndScene");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    
}

//Debug UI
#if UNITY_EDITOR

[CustomEditor(typeof(GameManager))]
class GameManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager debugger = (GameManager)target;

        if(GUILayout.Button("Game Start"))
        {
            debugger.GameStart();
        }
    }
}

#endif