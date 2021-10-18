using RhythmTool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Conductor : MonoBehaviour
{
    [Header("Assign References")]
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
    public float songLength { get; private set; }

    public RadialProgressBar songProgressBar;
    public BeatSlider slider;

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
    private int beatSetIndex = 1;
    private int beatIndex = 0;

    public float beatPercent { get; private set; }

    private List<float> beats = new List<float>();

    public delegate void SongBeatAction();
    public static event SongBeatAction OnSongBeat;

    private void OnEnable()
    {
        EventManager.OnBeatHit += OnBeatHit;
        OnSongBeat += ScaleBeatHitSlider;
    }
    private void OnDisable()
    {
        EventManager.OnBeatHit -= OnBeatHit;
        OnSongBeat -= ScaleBeatHitSlider;
    }

    private void Awake()
    {
        audioSource.clip = GameManager.Instance.currentSong.song;
        audioSource.Play();
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

        float songPercentage = audioSource.time / audioSource.clip.length;
        songProgressBar.SetRadialProgress(songPercentage);

        beatPercent = (songPosition - prevBeatTimestamp) / (nextBeatTimestamp - prevBeatTimestamp);

        if (songPosition >= nextBeatTimestamp)
        {
            beatSetIndex++;
            prevBeatTimestamp = nextBeatTimestamp;
            nextBeatTimestamp = beats[beatSetIndex * beatsPerSlide];
            hitBeatTimestamp = beats[beatSetIndex * beatsPerSlide - 1];

            moveSetGenerator.GenerateMoveSet(sequenceCount, arrowCount,arrowIncrement);

            //check if user missed beat hit once slider is complete
            if (!isBeatHitForTurn)
            {
                SpawnHitIndicator(HitAccuracy.Miss);
            }
            isBeatHitForTurn = false;
        }

        slider.SetSlidersValue(prevBeatTimestamp, nextBeatTimestamp, hitBeatTimestamp, songPosition);
        CheckOnBeat(songPosition);
    }

    public void CheckOnBeat(float songPos)
    {
        float offsetTime = 0.1f;

        if (Utilities.InRange(songPos, beats[beatIndex] - offsetTime, beats[beatIndex] + offsetTime))
        {
            OnSongBeat();
            beatIndex++;
        }
    }
    private void OnBeatHit()
    {
        bool isAnySequenceComplete = moveSetGenerator.IsAnySequenceComplete;
        int sequenceCount = moveSetGenerator.CompletedSequenceCount();

        float distance = Mathf.Abs(hitBeatTimestamp - songPosition);
        HitAccuracy beatHitAccuracy = GetHitAccuracy(distance);

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
            ScoreManager.UpdateScore(beatHitAccuracy, sequenceCount);

            isBeatHitForTurn = true;
        }
    }
    private void SpawnHitIndicator(HitAccuracy hitAccuracy)
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(hitAccuracy);
    }
    private HitAccuracy GetHitAccuracy(float distance)
    {
        if (Utilities.InRange(distance,0,0.05f))
        {
            return HitAccuracy.Perfect;
        }
        if (Utilities.InRange(distance, 0, 0.07f))
        {
            return HitAccuracy.Great;
        }
        if (Utilities.InRange(distance, 0, 0.1f))
        {
            return HitAccuracy.Cool;
        }
        return HitAccuracy.Miss;
    }
    private void ScaleBeatHitSlider()
    {
        slider.hitSliderKnob.DOPunchScale(new Vector3(0.5f,0.5f,0.5f), 0.3f, 3, 1);
    }
}
