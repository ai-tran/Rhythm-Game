using RhythmTool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Conductor : MonoBehaviour
{
    public TextMeshProUGUI percentText;

    [Header("Rhythm Tool Plugin")]
    public RhythmAnalyzer analyzer;
    public RhythmData rhythmData;

    [Header("Assign References")]
    public Slider beatSlider;
    public Slider hitSlider;
    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    private readonly List<Beat> beats = new List<Beat>();

    //Move set settings todo move into SO
    public MoveSetGenerator moveSetGenerator;
    public int sequenceCount = 3;
    public int arrowCount = 4;

    private HitAccuracy beatHitAccuracy;
    private bool isBeatHitForTurn = false;

    private float perfectPadding = 0.2f;

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
    public float firstBeatOffset = 3.90f;

    private float prevBeatTimestamp;
    private float nextBeatTimestamp;
    private float beatindex = 1;
    private float beatPerSlide = 3;
    private float beatPercent;
    public float hitBeatTimestamp;

    private float prevTime;

    private float userHitTimestamp;
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
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;

        prevBeatTimestamp = secPerBeat;
        nextBeatTimestamp = secPerBeat * beatPerSlide;

        rhythmData.GetFeatures<Beat>(beats, 0, 115.28f);
    }

    // Update is called once per frame
    private void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;

        beatPercent = (songPosition - prevBeatTimestamp) / (nextBeatTimestamp - prevBeatTimestamp);

        if (songPosition > nextBeatTimestamp)
        {
            beatindex++;
            prevBeatTimestamp = nextBeatTimestamp;
            nextBeatTimestamp = secPerBeat * (beatPerSlide * beatindex);
            hitBeatTimestamp = nextBeatTimestamp - secPerBeat;
            moveSetGenerator.GenerateMoveSet(sequenceCount, arrowCount);
            isBeatHitForTurn = false;
        }

        if (Utilities.InRange(hitBeatTimestamp, prevTime, songPosition))
        {
            print("Beat Hit");
        }

        SetSliderValue(prevBeatTimestamp, nextBeatTimestamp, hitBeatTimestamp, songPosition);

        prevTime = songPosition;

    }

    private void SetSliderValue(float sliderMin, float sliderMax, float hitValue, float sliderValue)
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
        if (!isBeatHitForTurn)
        {
            userHitTimestamp = songPosition;
            if (Utilities.InRange(userHitTimestamp, hitBeatTimestamp - perfectPadding, hitBeatTimestamp + perfectPadding))
            {
                beatHitAccuracy = HitAccuracy.Perfect;
            }
            else
            {
                beatHitAccuracy = HitAccuracy.Miss;
            }

            HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
            temp.Init(beatHitAccuracy);
            isBeatHitForTurn = true;
        }
    }
}
