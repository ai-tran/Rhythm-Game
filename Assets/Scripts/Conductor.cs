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

    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    public TextMeshProUGUI comboCounter;
    public TextMeshProUGUI scoreCounter;

    public int comboCount { get; private set; } = 0;
    public int scoreCount { get; private set; } = 0;

    //Move set settings todo move into SO
    public MoveSetGenerator moveSetGenerator;
    public int sequenceCount = 3;
    public int arrowCount = 4;

    private bool isBeatHitForTurn = false;

    private float hitPadding = 0.1f;

    [Tooltip("Song beats per minute")]
    public float songBpm = 96;
    [Tooltip("The number of seconds for each song beat")]
    public float beatsPerSec;
    [Tooltip("Current song position, in seconds")]
    public float songPosition;
    [Tooltip("Current song position, in beats")]
    public float songPositionInBeats;
    [Tooltip("How many seconds have passed since the song started")]
    public float dspSongTime;
    [Tooltip("The offset to the first beat of the song in seconds")]
    private float firstBeatOffset = 4.026f;

    public float hitOffset = 0f;

    private float prevBeatTimestamp;
    private float nextBeatTimestamp;
    private int beatindex = 1;
    private int beatsPerSlide = 3;
    public float beatPercent { get; private set; }
    public float hitBeatTimestamp;

    List<float> beats = new List<float>();

    public AudioSource audioSource;
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
        //init text 
        scoreCounter.text = scoreCount.ToString();
        comboCounter.text = ComboCountText(comboCount);
        
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

    // Update is called once per frame
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
            moveSetGenerator.GenerateMoveSet(sequenceCount, arrowCount);

            //check if user missed beat hit once slider is complete
            if (!isBeatHitForTurn)
            {
                SpawnHitIndicator(HitAccuracy.Miss);
            }

            isBeatHitForTurn = false;
        }

        SetSlidersValue(prevBeatTimestamp, nextBeatTimestamp, hitBeatTimestamp, songPosition);
    }

    private int Score(int hitValue, int comboMultiplier, int difficultyMultiplier)
    {
        int Score = hitValue + (hitValue * ((comboMultiplier * difficultyMultiplier) / 25));
        return Score;
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

        HitAccuracy beatHitAccuracy = GetHitAccuracy(hitAccuracyPercent);

        if (!isBeatHitForTurn)
        {
            if (isAnySequenceComplete && beatHitAccuracy != HitAccuracy.Miss)
            {
                SetComboCounter(comboCount + 1);
            }
            else
            {
                SetComboCounter(0);
            }

            SpawnHitIndicator(beatHitAccuracy);

            isBeatHitForTurn = true;
        }

        scoreCount += Score((int)beatHitAccuracy, comboCount, arrowCount);
        scoreCounter.text = scoreCount.ToString();
    }
    private void SetComboCounter(int count)
    {
        if (count > comboCount)
        {
            comboCounter.rectTransform.DOPunchScale(Vector3.one, 0.2f, 10, 1);
        }
        comboCounter.text = ComboCountText(count);
        comboCount = count;
    }
    private string ComboCountText(int count)
    {
        string text = "x" + count;
        return text;
    }
    private void SpawnHitIndicator(HitAccuracy hitAccuracy)
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(hitAccuracy);
    }
    private HitAccuracy GetHitAccuracy(float percent)
    {
        if (percent >= 0.9f)
        {
            return HitAccuracy.Perfect;
        }
        if (percent >= 0.8f)
        {
            return HitAccuracy.Great;
        }
        return HitAccuracy.Miss;
    }
    private void DebugSongSync()
    {
        print("curr song pos: " + songPosition);
        print("beat timestamp: " + hitBeatTimestamp);
        print("prev beat timestamp: " + prevBeatTimestamp);
        print("next beat timestamp: " + nextBeatTimestamp);
    }
}
