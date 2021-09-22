using RhythmTool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private HitAccuracy beatHitAccuracy;
    private bool isBeatHitForTurn = false;

    private float perfectPadding = 0f;

    [Tooltip("Song beats per minute")]
    public float songBpm = 96;
    [Tooltip("The number of seconds for each song beat")]
    public float secPerBeat;
    [Tooltip("Current song position, in seconds")]
    public float songPosition;
    [Tooltip("Current song position, in beats")]
    public float songPositionInBeats;
    [Tooltip("How many seconds have passed since the song started")]
    public float dspSongTime;
    [Tooltip("The offset to the first beat of the song in seconds")]
    private float firstBeatOffset = 4.026f;

    public float hitOffset = 0.1f;

    private float prevBeatTimestamp;
    private float nextBeatTimestamp;
    private int beatindex = 1;
    private int beatsPerSlide = 5;
    public float beatPercent { get; private set; }
    public float hitBeatTimestamp;
    private float prevTime;

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
        comboCounter.text = comboCount.ToString();

        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;

        for(float i = firstBeatOffset; i <= audioSource.clip.length; i += secPerBeat)
        {
            beats.Add(i);
        }

        prevBeatTimestamp = firstBeatOffset;
        nextBeatTimestamp = beats[beatsPerSlide];
        hitBeatTimestamp = beats[beatsPerSlide - 1];
    }

    // Update is called once per frame
    private void Update()
    {
        songPosition = audioSource.time;
        //songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

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
                beatHitAccuracy = HitAccuracy.Miss;
                SpawnHitIndicator(HitAccuracy.Miss);
            }

            isBeatHitForTurn = false;
        }

        SetSlidersValue(prevBeatTimestamp, nextBeatTimestamp, hitBeatTimestamp, songPosition);
        prevTime = songPosition;
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
        scoreCount += Score((int)beatHitAccuracy, comboCount, arrowCount);
        scoreCounter.text = scoreCount.ToString();

        print("curr song pos: " + songPosition);
        print("beat timestamp: " + hitBeatTimestamp);
        print("prev beat timestamp: " + prevBeatTimestamp);
        print("next beat timestamp: " + nextBeatTimestamp);

        if (!isBeatHitForTurn)
        {
                beatHitAccuracy = HitAccuracy.Perfect;
                comboCount++;

            SpawnHitIndicator(beatHitAccuracy);
            comboCounter.text = "x" + comboCount.ToString();

            isBeatHitForTurn = true;
        }
    }

    private void ComboCount(int count)
    {

    }

    private void SpawnHitIndicator(HitAccuracy hitAccuracy)
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(hitAccuracy);
    }
}
