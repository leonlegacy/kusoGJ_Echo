using System;
using UnityEngine;

namespace CliffLeeCL
{
    /// <summary>
    /// This singleton class manage all events in the game.
    /// </summary>
    /// <code>
    /// // Usage in other class example:\n
    /// void Start(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// // If OnEnable function will cause error, try listen to events in Start function.\n
    /// void OnEnable(){\n
    ///     EventManager.instance.onGameOver += LocalFunction;\n
    /// }\n
    /// \n
    /// void OnDisable(){\n
    ///     EventManager.instance.onGameOver -= LocalFunction;\n
    /// }\n
    /// \n
    /// void LocalFunction(){\n
    ///     //Do something here\n
    /// }
    /// </code>
    public class EventManager : Singleton<EventManager>
    {
        /// <summary>
        /// The event is called when game start.
        /// </summary>
        public event Action onGameStart;
        
        /// <summary>
        /// The event is called when game over.
        /// </summary>
        /// <seealso cref="OnGameOver"/>
        public event Action onGameOver;
        /// <summary>
        /// The event is called when a player scored. Only can call on the server(SyncEvent).
        /// </summary>
        /// <seealso cref="OnPlayerScored"/>
        public event Action onPlayerScored;

        public event Action onNewGameLoad;

        public event Action<int> onMusicPlay;

        public event Action onStopAudience;
        public void OnGameStart()
        {
            onGameStart?.Invoke();
            Debug.Log("OnGameStart event is invoked!");
        }

        /// <summary>
        /// The function is called when a player scored.
        /// </summary>
        /// <seealso cref="onGameOver"/>
        public void OnGameOver()
        {
            onGameOver?.Invoke();
            Debug.Log("OnGameOver event is invoked!");
        }

        /// <summary>
        /// The function is called when a player scored.
        /// </summary>
        /// <seealso cref="onPlayerScored"/>
        public void OnPlayerScored()
        {
            onPlayerScored?.Invoke(); 
        }

        public void OnMusicPlay(int musicId = 0)
        {
            onMusicPlay?.Invoke(musicId);
        }

        public void OnStopAudience()
        {
            onStopAudience?.Invoke();
        }

        public void OnNewGameLoad()
        {
            onNewGameLoad?.Invoke();
        }
    }
}

