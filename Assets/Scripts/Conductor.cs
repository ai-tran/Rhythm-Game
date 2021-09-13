using DG.Tweening;
using RhythmTool;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public RhythmAnalyzer analyzer;
    public AudioSource audioSource;
    public RhythmData rhythmData;
    public SpriteRenderer character;
    public BeatCounter beatCounter;

    private float prevTime = 0;
    public List<Beat> beats = new List<Beat>();
    
    private BeatCount[] beatCounters;
    private int beatCounterIndex = 0;

    void Awake()
    {
        beatCounters = beatCounter.beatCounters;

        //Find a track with Beats.
        Track<Beat> track = rhythmData.GetTrack<Beat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(beatCounterIndex >= beatCounters.Length)
        {
            beatCounterIndex = 0;
        }

        //Clear the list.
        beats.Clear();
        float time = audioSource.time;

        //Find all beats for the part of the song that is currently playing.
        rhythmData.GetFeatures(beats, prevTime, time);

        //Do something with the Beats here.
        foreach (Beat beat in beats)
        {
            beatCounters[beatCounterIndex].OnBeat();

            if (beatCounterIndex != 0)
            {
                int previousIndex = beatCounterIndex - 1;
                beatCounters[previousIndex].OffBeat();
            }
            else
            {
                int lastIndex = beatCounters.Length -1;
                beatCounters[lastIndex].OffBeat();
            }

            beatCounterIndex++;
        }
        //Keep track of the previous playback time of the AudioSource.
        prevTime = time;

        if (Input.GetKeyDown(KeyCode.Space)){
            //temp move this from being hardcoded
            if(beatCounterIndex == 9)
            {
                print("Hit");
            }
            else
            {
                print("Miss");
            }
        }
    }
}
