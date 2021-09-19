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

    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    private float prevTime = 0;
    private readonly List<Beat> beats = new List<Beat>();

    //Beat counter stuff
    public int beatCount = 5;
    public float horizontalOffset = 0.3f;
    public BeatCounter beatCounter;
    private BeatCount[] beatCounts;
    private int beatCounterIndex = 0;
    public int hitIndex = 0;

    //Move set settings
    public MoveSetGenerator moveSetGenerator;
    public int sequenceCount = 3;
    public int arrowCount = 4;

    private HitAccuracy beatHitAccuracy;
    private bool isBeatHit = false;

    private void OnEnable()
    {
        EventManager.OnBeatHit += OnBeatHit;
    }

    private void OnDisable()
    {
        EventManager.OnBeatHit -= OnBeatHit;
    }

    private void Awake()
    {
        beatCounter.Init(hitIndex, beatCount);
        beatCounts = beatCounter.beatCounts;
    }

    // Update is called once per frame
    private void Update()
    {
        if (beatCounterIndex >= beatCounts.Length)
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
            isBeatHit = false;
            beatCounts[beatCounterIndex].OnBeat();

            if (beatCounterIndex == 0)
            {
                beatCounts[beatCounterIndex]
                    .OnComplete(() => moveSetGenerator.GenerateMoveSet(sequenceCount,arrowCount));
            }

            if (beatCounterIndex != 0)
            {
                int previousIndex = beatCounterIndex - 1;
                beatCounts[previousIndex].OffBeat();
            }
            else
            {
                int lastIndex = beatCounts.Length - 1;
                beatCounts[lastIndex].OffBeat();
            }

            beatCounterIndex++;
        }
        //Keep track of the previous playback time of the AudioSource.
        prevTime = time;

        if (beatCounterIndex == hitIndex)
        {
            beatHitAccuracy = HitAccuracy.Perfect;
        }
        else
        {
            beatHitAccuracy = HitAccuracy.Miss;
        }

    }

    private void OnBeatHit()
    {
        if (!isBeatHit)
        {
            HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
            temp.Init(beatHitAccuracy);
            isBeatHit = true;
        }
    }
}
