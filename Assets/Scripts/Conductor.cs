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
    private List<Beat> beats = new List<Beat>();

    //Beat counter stuff
    public int beatCounterCount = 5;
    public GameObject beatCountPrefab;
    public float horizontalOffset = 0.3f;
    public BeatCounter beatCounter;
    private BeatCount[] beatCounters;
    private int beatCounterIndex = 0;
    public int beatHitMarker = 0;

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

    private void Start()
    {
        beatCounter = GameObject.FindObjectOfType<BeatCounter>();
        beatCounters = beatCounter.beatCounters;
        SpawnBeatCounter(beatCounterCount, horizontalOffset);
    }

    private void SpawnBeatCounter(int count, float offset)
    {
        for(int i = 0; i < count; i++)
        {
            //BeatCount temp = Instantiate()
        }
    }

    // Update is called once per frame
    private void Update()
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
            isBeatHit = false;
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
