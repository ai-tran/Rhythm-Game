using RhythmTool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Conductor : MonoBehaviour
{
    [Header("Rhythm Tool Plugin")]
    public RhythmAnalyzer analyzer;
    public RhythmData rhythmData;

    [Header("Assign References")]
    public Slider beatSlider;
    public Slider hitSlider;
    public AudioSource audioSource;
    public ScoreManager ScoreManager
    {
        get
        {
            if (scoreManager == null)
            {
                var instance = FindObjectOfType<ScoreManager>();
                scoreManager = instance;
            }
            return scoreManager;
        }
    }
    public ScoreManager scoreManager;


    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    //Move set settings todo move into SO
    public MoveSetGenerator moveSetGenerator;
    public int sequenceCount = 3;
    public int arrowCount = 4;
    public int arrowIncrement = 1;

    private bool isBeatHitForTurn = false;

    //The number of seconds for each song beat
    private float beatsPerSec;
    public float songPosition;

    public delegate void CompleteBeatHit();
    public static event CompleteBeatHit OnCompleteBeatHit;
    private float songBpm
    {
        get
        {
            return GameManager.Instance.currentSong.bpm;
        }
    }
    private float firstBeatOffset { get
        {
            return GameManager.Instance.currentSong.firstBeatOffset;
        } 
    }
    private int beatsPerSlide
    {
        get
        {
            return GameManager.Instance.currentSong.beatsPerSlide;
        }
    }

    private float hitBeatTimestamp;
    private float prevBeatTimestamp;
    private float nextBeatTimestamp;
    private int beatindex = 1;

    public float beatPercent { get; private set; }

    private List<float> beats = new List<float>();

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

        beatsPerSec = 60f / songBpm;

        for(float i = firstBeatOffset; i <= audioSource.clip.length; i += beatsPerSec)
        {
            beats.Add(i);
        }

        //setup initial timestamps
        prevBeatTimestamp = firstBeatOffset;
        nextBeatTimestamp = beats[beatsPerSlide];
        hitBeatTimestamp = beats[beatsPerSlide - 1];
    }

    private void Update()
    {
        songPosition = audioSource.time;

        beatPercent = (songPosition - prevBeatTimestamp) / (nextBeatTimestamp - prevBeatTimestamp);

        if (songPosition >= nextBeatTimestamp)
        {
            beatindex++;
            prevBeatTimestamp = nextBeatTimestamp;
            nextBeatTimestamp = beats[beatindex * beatsPerSlide];
            hitBeatTimestamp = beats[beatindex * beatsPerSlide - 1];

            moveSetGenerator.GenerateMoveSet(sequenceCount, arrowCount,arrowIncrement);

            //check if user missed beat hit once slider is complete
            if (!isBeatHitForTurn)
            {
                SpawnHitIndicator(HitAccuracy.Miss);
            }

            isBeatHitForTurn = false;
        }

        SetSlidersValue(prevBeatTimestamp, nextBeatTimestamp, hitBeatTimestamp, songPosition);
    }

    private void SetSlidersValue(float sliderMin, float sliderMax, float hitValue, float sliderValue)
    {
        beatSlider.minValue = sliderMin;
        beatSlider.maxValue = sliderMax;

        hitSlider.minValue = sliderMin;
        hitSlider.maxValue = sliderMax;

        hitSlider.value = hitValue;
        beatSlider.value = sliderValue;
    }
    private void OnBeatHit()
    {
        bool isAnySequenceComplete = moveSetGenerator.IsAnySequenceComplete;

        float hitAccuracyPercent = (songPosition > hitBeatTimestamp) ?
        (songPosition - nextBeatTimestamp) / (hitBeatTimestamp - nextBeatTimestamp) :
        (songPosition - prevBeatTimestamp) / (hitBeatTimestamp - prevBeatTimestamp);

        float distance = Mathf.Abs(hitBeatTimestamp - songPosition);

        HitAccuracy beatHitAccuracy = GetHitAccuracy(hitAccuracyPercent);

        if (!isBeatHitForTurn)
        {
            if (isAnySequenceComplete && beatHitAccuracy != HitAccuracy.Miss)
            {
                OnCompleteBeatHit.Invoke();
            }
            else
            {
                beatHitAccuracy = HitAccuracy.Miss;
            }

            SpawnHitIndicator(beatHitAccuracy);

            isBeatHitForTurn = true;
        }
    }

    private void SpawnHitIndicator(HitAccuracy hitAccuracy)
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        ScoreManager.UpdateScore(hitAccuracy, 1);
        temp.Init(hitAccuracy);
    }
    private HitAccuracy GetHitAccuracy(float percent)
    {
        if (percent >= 0.97f)
        {
            return HitAccuracy.Perfect;
        }
        if (percent >= 0.96f)
        {
            return HitAccuracy.Great;
        }
        if (percent >= 0.90f)
        {
            return HitAccuracy.Cool;
        }
        return HitAccuracy.Miss;
    }
}
