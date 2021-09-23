using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/Song", order = 2)]
public class Song : ScriptableObject
{
    public string Title;
    public string Author;
    public AudioClip song;
    public float bpm;
    [Tooltip("The offset to the first beat of the song in seconds")]
    public float firstBeatOffset;
    public int beatsPerSlide;
}
