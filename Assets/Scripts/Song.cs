using System;
using CliffLeeCL;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable][CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/Song", order = 1)]
public class Song : SerializedScriptableObject
{
    public AudioManager.AudioName audioName;
    public string songName;
    public float duration;
    public int beatsPerMinute;
}
