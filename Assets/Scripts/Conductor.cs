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

    public Slider beatSlider;
    public Slider hitSlider;

    public AudioSource audioSource;

    public SpriteRenderer character;

    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    private readonly List<Beat> beats = new List<Beat>();

    //Move set settings
    public MoveSetGenerator moveSetGenerator;
    public int sequenceCount = 3;
    public int arrowCount = 4;

    private HitAccuracy beatHitAccuracy;
    private bool isBeatHit = false;

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
    private float beatPerSlide = 5;
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

        IncrementBeatSet();

        if (Utilities.InRange(hitBeatTimestamp, prevTime, songPosition))
        {
            print("Beat Hit");
        }

        beatSlider.minValue = prevBeatTimestamp;
        beatSlider.maxValue = nextBeatTimestamp;

        hitSlider.minValue = prevBeatTimestamp;
        hitSlider.maxValue = nextBeatTimestamp;

        hitSlider.value = hitBeatTimestamp;
        beatSlider.value = songPosition;


        prevTime = songPosition;
    }

    /// <summary>
    /// Update the slider to head to the next set of beats and reset next index to current index
    /// </summary>
    private void IncrementBeatSet()
    {
        if (songPosition > nextBeatTimestamp)
        {
            beatindex++;
            prevBeatTimestamp = nextBeatTimestamp;
            nextBeatTimestamp = secPerBeat * (beatPerSlide * beatindex);
            hitBeatTimestamp = nextBeatTimestamp - secPerBeat;
            moveSetGenerator.GenerateMoveSet(sequenceCount, arrowCount);
        }
    }

    private void OnBeatHit()
    {
        userHitTimestamp = songPosition;
        if(Utilities.InRange(userHitTimestamp, hitBeatTimestamp - perfectPadding, hitBeatTimestamp + perfectPadding)){
            beatHitAccuracy = HitAccuracy.Perfect;
        }
        else
        {
            beatHitAccuracy = HitAccuracy.Miss;
        }

        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(beatHitAccuracy);
    }
}
