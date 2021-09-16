using DG.Tweening;
using RhythmTool;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [Header("Rhythm Tool Plugin")]
    public RhythmAnalyzer analyzer;
    public RhythmData rhythmData;


    public AudioSource audioSource;

    public SpriteRenderer character;
    public BeatCounter beatCounter;

    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    private float prevTime = 0;
    public List<Beat> beats = new List<Beat>();
    
    private BeatCount[] beatCounters;
    private int beatCounterIndex = 0;

    private HitAccuracy beatHitAccuracy;

    void OnEnable()
    {
        EventManager.OnBeatHit += OnBeatHit;
        EventManager.OnArrowKeyPress += OnArrowKeyPress;
    }

    private void OnDisable()
    {
        EventManager.OnBeatHit -= OnBeatHit;
        EventManager.OnArrowKeyPress -= OnArrowKeyPress;
    }

    void Awake()
    {
        beatCounters = beatCounter.beatCounters;
    }

    // Update is called once per frame
    void Update()
    {
        if (beatCounterIndex >= beatCounters.Length)
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
                int lastIndex = beatCounters.Length - 1;
                beatCounters[lastIndex].OffBeat();
            }

            beatCounterIndex++;
        }
        //Keep track of the previous playback time of the AudioSource.
        prevTime = time;

        if (beatCounterIndex == 8)
        {
            beatHitAccuracy = HitAccuracy.Perfect;
        }
        else
        {
            beatHitAccuracy = HitAccuracy.Miss;
        }

    }

    void OnBeatHit()
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(beatHitAccuracy);
    }

    void SetHitIndicator()
    {
        
    }

    void OnArrowKeyPress(Direction direction)
    {

    }
}
