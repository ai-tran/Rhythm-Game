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

    public Slider slider;

    public AudioSource audioSource;

    public SpriteRenderer character;

    public Transform hitAccuracyPrefab;
    public Transform HitAccuracySpawn;

    private float beatHitPercent = 0.8f;
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

    int beatIndex = 0;
    int nextIndex = 1;

    //the current position of the song (in seconds)
    float songPosition;

    //the current position of the song (in beats)
    float songPosInBeats;

    //the duration of a beat
    float secPerBeat;

    //how much time (in seconds) has passed since the song started
    float dsptimesong;

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
        //Find all beats for the part of the song that is currently playing.
        rhythmData.GetFeatures<Beat>(beats, 0, 115.28f);
        audioSource.pitch = 1f;
        //print(beats.Count);

    }

    // Update is called once per frame
    private void Update()
    {
        if (beatIndex >= beats.Count)
        {
            return;
        }

        //Get the current playback time of the AudioSource.
        float time = audioSource.time;
        float prevBeatTime = beatIndex == 0 ? 0f : beats[beatIndex - 1].timestamp;
        float nextBeatTime = beats[beatIndex].timestamp;
        
        // this is the beat percent between the last beat and current beat
        float beatPercent = (time - prevBeatTime) / (nextBeatTime - prevBeatTime);
        percentText.text = beatPercent.ToString();

        float sliderPercent = Mathf.Min(beatPercent, 1f);

        if (beatIndex == 0)
        {
            sliderPercent *= beatHitPercent;
        }
        else
        {
            sliderPercent -= (1f - beatHitPercent);

            if (sliderPercent < 0f)
            {
                sliderPercent = beatHitPercent + (1f - beatHitPercent + sliderPercent);
            }
        }

        slider.value = sliderPercent;

        if (beatPercent >= 1f)
        {
            print("beat");
            beatIndex++;
        }
    }

    private void OnBeatHit()
    {
        HitAccuracyIndicator temp = Instantiate(hitAccuracyPrefab).GetComponent<HitAccuracyIndicator>();
        temp.Init(beatHitAccuracy);
    }
}
